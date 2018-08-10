module Aktivitaeten.State

open System
open Elmish
open Types
open Fable.PowerPack.Fetch

let rand = Random()

let init : Model * Cmd<Msg> =
  let loadStallzeitenCmd =
    Cmd.ofPromise
      (fetchAs<DateTime list> (sprintf "api/stallzeiten?rand=%f" (rand.NextDouble())))
      []
      LoadStallzeitenSuccess
      (HttpError >> LoadStallzeitenError)
  { Stallzeiten = Loading }, loadStallzeitenCmd

let update msg model =
  match msg with
  | LoadStallzeitenSuccess items ->
     { model with Stallzeiten = Loaded items }, []
  | LoadStallzeitenError error ->
     { model with Stallzeiten = LoadError error }, []
