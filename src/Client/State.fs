module App.State

open Browser.Dom
open Browser.Types
open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Fable.Core.JsInterop
open Global
open Types

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map Aktivitaeten (s "aktivitaeten")
    map (UeberDenHof OpenMenusExpanded) (s "ueber-den-hof")
    map (UeberDenHof AllMenusExpanded) (s "ueber-den-hof" </> s "expand-all")
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
      model, Navigation.modifyUrl (toHash model.CurrentPage)
    )
  
  window.document.title <- toString model'.CurrentPage |> sprintf "%s | Enserhof z'Ehrndorf"

  do
    let elem =
      match document.querySelector "meta[rel=canonical]" with
      | e when not !!e ->
        let elem = document.createElement "meta"
        elem.setAttribute("rel", "canonical")
        document.head.appendChild elem :?> Element
      | e -> e
    elem.setAttribute("href", sprintf "https://enserhof.github.io%s" (toUrl model'.CurrentPage))

  do
    let elem =
      match document.querySelector "meta[rel=alternate]" with
      | e when not !!e ->
        let elem = document.createElement "meta"
        elem.setAttribute("rel", "alternate")
        elem.setAttribute("hreflang", "de")
        document.head.appendChild elem :?> Element
      | e -> e
    elem.setAttribute("href", sprintf "https://enserhof.github.io%s" (toUrl model'.CurrentPage))

  GTag.setPage GTag.trackingId (toUrl model'.CurrentPage)

  model', cmd'

let init result =
  let aktivitaeten, aktivitaetenCmd = Aktivitaeten.State.init
  let ueberDenHof, ueberDenHofCmd = UeberDenHof.State.init
  let administration, administrationCmd = Administration.State.init
  let model = {
    CurrentPage = Aktivitaeten
    Aktivitaeten = aktivitaeten
    UeberDenHof = ueberDenHof
    Administration = administration
  }
  let model', cmd = urlUpdate result model
  let cmd' =
    Cmd.batch [
      cmd
      Cmd.map AktivitaetenMsg aktivitaetenCmd
      Cmd.map UeberDenHofMsg ueberDenHofCmd
      Cmd.map AdministrationMsg administrationCmd
    ]
  model', cmd'

let update msg model =
  match msg with
  | AktivitaetenMsg msg' ->
    let subModel, subCmd = Aktivitaeten.State.update msg' model.Aktivitaeten
    { model with Aktivitaeten = subModel }, Cmd.map AktivitaetenMsg subCmd
  | UeberDenHofMsg msg' ->
    let subModel, subCmd = UeberDenHof.State.update msg' model.UeberDenHof
    { model with UeberDenHof = subModel }, Cmd.map UeberDenHofMsg subCmd
  | AdministrationMsg msg' ->
    let subModel, subCmd = Administration.State.update msg' model.Administration
    { model with Administration = subModel }, Cmd.map AdministrationMsg subCmd
