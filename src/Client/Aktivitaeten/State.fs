module Aktivitaeten.State

open Elmish
// open Thoth.Fetch
// open Thoth.Json
open System
open Types

let private parseStallzeit (text: string) =
  match DateTime.TryParse(text) with
  | (true, v) -> Timestamp v
  | (false, _) -> InfoText text

let private parseStallzeiten (text: string) =
  text.Split([|'\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
  |> Seq.map parseStallzeit
  |> Seq.toList

let init : Model * Cmd<Msg> =
  let loadStallzeitenCmd =
    Cmd.OfPromise.either
      (fun () ->
        Fetch.fetch "api/stallzeiten" []
        |> Promise.bind (fun r -> r.text())
        |> Promise.map parseStallzeiten)
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
