module UeberDenHof.State

open Elmish
open Types

let init =
  { OpenMenus = Set.empty }, []

let update msg model =
  match msg with
  | OpenMenu item ->
     { model with OpenMenus = Set.add item model.OpenMenus }, []
  | CloseMenu item ->
     { model with OpenMenus = Set.remove item model.OpenMenus }, []
  | ExpandAllMenus ->
    { model with OpenMenus = Set.ofList allMenuItems }, []
