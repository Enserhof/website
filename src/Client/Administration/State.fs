module Administration.State

open Elmish
open Elmish.Toastr
open System
open Thoth.Fetch
open Thoth.Json
open Types
open Types.GitHubApi

let private tryGetGitHubAccessToken () =
  Browser.WebStorage.localStorage.getItem "GITHUB_ACCESS_TOKEN"
  |> Option.ofObj

let private setGitHubAccessToken value =
  Browser.WebStorage.localStorage.setItem("GITHUB_ACCESS_TOKEN", value)

let stallzeitenRemoteUrl = "https://api.github.com/repos/enserhof/enserhof.github.io/contents/src/Server/api/stallzeiten?ref=dev"

let rec update msg model =
  match msg with
  | LoadStallzeiten ->
    let cmd =
      Cmd.OfPromise.either
        (fun () ->
          let properties = [ Fetch.requestHeaders [ Fetch.Types.Authorization (sprintf "Bearer %s" model.GitHubAccessToken) ] ]
          Fetch.fetchAs (stallzeitenRemoteUrl, GetContentResponse.decoder, properties)
        )
        ()
        LoadStallzeitenSuccess
        (LoadStallzeitenError.HttpError >> LoadStallzeitenError)
    { model with RemoteStallzeiten = Loading }, cmd
  | LoadStallzeitenSuccess file ->
    file.Content
    |> Browser.Dom.window.atob
    |> Decode.fromString (Decode.list Aktivitaeten.Types.Stallzeit.decoder)
    |> function
    | Ok stallzeiten ->
      let model' =
        { model with
            RemoteStallzeiten = Loaded { Version = file.Sha; FileUrl = file.Url; Stallzeiten = stallzeiten }
            LocalStallzeiten =
              stallzeiten
              |> List.map (fun timestamp ->
                let stallzeitValue =
                  match timestamp with
                  | Aktivitaeten.Types.Timestamp v -> Timestamp (Valid v)
                  | Aktivitaeten.Types.InfoText v -> InfoText v
                { Id = Guid.NewGuid().ToString(); Value = stallzeitValue })
        }
      model', []
    | Error e ->
      update (LoadStallzeitenError (ParseError e)) model
  | LoadStallzeitenError ((LoadStallzeitenError.HttpError e) as error) ->
    Browser.Dom.console.error("Error while loading Stallzeiten: ", e)
    { model with RemoteStallzeiten = LoadError error; LocalStallzeiten = [] }, []
  | LoadStallzeitenError ((ParseError e) as error) ->
    Browser.Dom.console.error("Error while parsing Stallzeiten: ", e)
    { model with RemoteStallzeiten = LoadError error; LocalStallzeiten = [] }, []
  | UpdateGitHubAccessToken value ->
    { model with GitHubAccessToken = value }, []
  | Login ->
    setGitHubAccessToken model.GitHubAccessToken
    update LoadStallzeiten model
  | UpdateStallzeit (timeId, value) ->
    let updateTime stallzeit =
      if timeId = stallzeit.Id
      then { Id = timeId; Value = value }
      else stallzeit
    { model with LocalStallzeiten = model.LocalStallzeiten |> List.map updateTime }, []
  | AddStallzeitTimestamp ->
    let newStallzeit = {
      Id = Guid.NewGuid().ToString()
      Value = Timestamp (Valid (DateTime.Today.AddDays(1.).AddHours(8.5))) }
    { model with LocalStallzeiten = model.LocalStallzeiten @ [ newStallzeit ] }, []
  | AddStallzeitInfoText ->
    let newStallzeit = {
      Id = Guid.NewGuid().ToString()
      Value = InfoText "" }
    { model with LocalStallzeiten = model.LocalStallzeiten @ [ newStallzeit ] }, []
  | RemoveStallzeit timeId ->
    let stallzeiten' =
      model.LocalStallzeiten
      |> List.filter (fun t -> t.Id <> timeId)
    { model with LocalStallzeiten = stallzeiten' }, []
  | SaveStallzeiten ->
    let save url version =
      let stallzeiten' =
        model.LocalStallzeiten
        |> List.choose (fun item ->
          match item.Value with
          | Timestamp (Valid value) -> Some (Aktivitaeten.Types.Timestamp value)
          | Timestamp (Invalid _) -> None
          | InfoText v -> Some (Aktivitaeten.Types.InfoText v)
        )
      let encode = List.map Aktivitaeten.Types.Stallzeit.encode >> Encode.list
      let body = {
        Message = "Update Stallzeiten"
        Content = Encode.toString 0 (encode stallzeiten') |> Browser.Dom.window.btoa
        Sha = version
        Branch = "dev"
      }
      let cmd =
        Cmd.OfPromise.either
          (fun () ->
            let properties = [ Fetch.requestHeaders [ Fetch.Types.Authorization (sprintf "Bearer %s" model.GitHubAccessToken) ] ]
            Fetch.put (url, SetContentRequest.encode body, SetContentResponse.decoder, properties))
          ()
          SaveStallzeitenSuccess
          (HttpError >> SaveStallzeitenError)
      model, cmd
    match model.RemoteStallzeiten with
    | Loaded remoteStallzeiten ->
      save remoteStallzeiten.FileUrl (Some remoteStallzeiten.Version)
    | LoadError _ ->
      save stallzeitenRemoteUrl None
    | _ -> model, []
  | SaveStallzeitenSuccess response ->
    match model.RemoteStallzeiten with
    | Loaded stallzeiten ->
      let model' =
        { model with
            RemoteStallzeiten =
              Loaded
                { stallzeiten with
                    Version = response.Sha
                }
        }
      let cmd =
        Toastr.message "Stallzeiten wurden erfolgreich gespeichert"
        |> Toastr.title "Stallzeiten speichern"
        |> Toastr.showCloseButton
        |> Toastr.success
      model', cmd
    | _ -> model, []
  | SaveStallzeitenError (HttpError e) ->
    Browser.Dom.console.error("Error while saving Stallzeiten: ", e)
    let cmd =
      Toastr.message "Fehler beim Speichern der Stallzeiten"
      |> Toastr.title "Stallzeiten speichern"
      |> Toastr.showCloseButton
      |> Toastr.error
    model, cmd

let init =
  let gitHubAccessToken = tryGetGitHubAccessToken()
  let model = {
    GitHubAccessToken = gitHubAccessToken |> Option.defaultValue ""
    RemoteStallzeiten = NotLoaded
    LocalStallzeiten = [] }
  match gitHubAccessToken with
  | Some _ -> update LoadStallzeiten model
  | None -> model, Cmd.none
