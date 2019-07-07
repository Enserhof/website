module Aktivitaeten.State

open Elmish
open Thoth.Fetch
open Thoth.Json
open Types

let init : Model * Cmd<Msg> =
  let loadStallzeitenCmd =
    Cmd.OfPromise.either
      (fun () -> Fetch.fetchAs ("api/stallzeiten", Decode.list Stallzeit.decoder))
      ()
      LoadStallzeitenSuccess
      (HttpError >> LoadStallzeitenError)
  { Stallzeiten = Loading }, loadStallzeitenCmd

let update msg model =
  match msg with
  | LoadStallzeitenSuccess items ->
     { model with Stallzeiten = Loaded items }, []
  | LoadStallzeitenError error ->
     { model with Stallzeiten = LoadError error }, []
