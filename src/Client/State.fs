module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open Global
open Types

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map UeberDenHof (s "ueber-den-hof")
    map Aktivitaeten (s "aktivitaeten")
    map Lageplan (s "lageplan")
    map Administration (s "administration")
  ]

let urlUpdate (page: Option<Page>) model =
  let (model', cmd') =
    page
    |> FSharp.Core.Option.map (fun page ->
      { model with CurrentPage = page }, Cmd.none
    )
    |> FSharp.Core.Option.defaultWith (fun () ->
      console.error("Error parsing url")
      model, Navigation.modifyUrl (toHash model.CurrentPage)
    )
  
  Fable.Import.Browser.window.document.title <- toString model'.CurrentPage |> sprintf "%s | Enserhof z'Ehrndorf"

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
