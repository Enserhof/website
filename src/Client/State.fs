module App.State

open Browser.Dom
open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Fable.Core
open Global
open Types

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map Startseite top
    map Angebote (s "angebote")
    map (Option.map (fun _ -> UeberDenHof AllMenusExpanded) >> Option.defaultValue (UeberDenHof OpenMenusExpanded)) (s "ueber-den-hof" <?> stringParam "expand-all")
    map Lageplan (s "lageplan")
    map Administration (s "administration")
  ]

let urlUpdate (page: Option<Page>) model =
  let (model', cmd') =
    page
    |> Option.map (fun page ->
      match page with
      | UeberDenHof AllMenusExpanded ->
        let subModel, subCmd =
          UeberDenHof.State.update UeberDenHof.Types.ExpandAllMenus model.UeberDenHof
        { model with
            CurrentPage = page
            UeberDenHof = subModel
        }, Cmd.map UeberDenHofMsg subCmd
      | _ ->
        { model with CurrentPage = page }, Cmd.none
    )
    |> Option.defaultWith (fun () ->
      console.error("Error parsing url")
      model, Navigation.modifyUrl (toUrl model.CurrentPage)
    )

  window.document.title <- toString model'.CurrentPage |> sprintf "%s | Enserhof z'Ehrndorf"

  model', cmd'

let init result =
  let angebote, angeboteCmd = Angebote.State.init
  let ueberDenHof, ueberDenHofCmd = UeberDenHof.State.init
  let administration, administrationCmd = Administration.State.init
  let model = {
    CurrentPage = Angebote
    Angebote = angebote
    UeberDenHof = ueberDenHof
    Administration = administration
  }
  let model', cmd = urlUpdate result model
  let cmd' =
    Cmd.batch [
      cmd
      Cmd.map AngeboteMsg angeboteCmd
      Cmd.map UeberDenHofMsg ueberDenHofCmd
      Cmd.map AdministrationMsg administrationCmd
    ]
  model', cmd'

// Navigation.newUrl doesn't work, because constructing the custom event yields a JS error
module Navigation' =
    [<Emit("new CustomEvent($0)")>]
    let private createCustomEvent name = jsNative

    let newUrl url =
        [
            fun _ ->
                history.pushState((), "", url)
                let ev = createCustomEvent "NavigatedEvent"
                window.dispatchEvent(ev) |> ignore
        ]

let update msg model =
  match msg with
  | ShowPage page ->
    model, Navigation'.newUrl (toUrl page)
  | AngeboteMsg msg' ->
    let subModel, subCmd = Angebote.State.update msg' model.Angebote
    { model with Angebote = subModel }, Cmd.map AngeboteMsg subCmd
  | UeberDenHofMsg msg' ->
    let subModel, subCmd = UeberDenHof.State.update msg' model.UeberDenHof
    { model with UeberDenHof = subModel }, Cmd.map UeberDenHofMsg subCmd
  | AdministrationMsg msg' ->
    let subModel, subCmd = Administration.State.update msg' model.Administration
    { model with Administration = subModel }, Cmd.map AdministrationMsg subCmd
