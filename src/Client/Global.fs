module Global

open Fable.MomentJs

moment.locale "de" |> ignore

type UeberDenHofMenuState =
  | OpenMenusExpanded
  | AllMenusExpanded

type Page =
  | Aktivitaeten
  | UeberDenHof of UeberDenHofMenuState
  | Lageplan
  | Administration

let publicPages =
  [
    Aktivitaeten
    UeberDenHof OpenMenusExpanded
    Lageplan
  ]

let toUrl page =
  match page with
  | Aktivitaeten -> "/aktivitaeten"
  | UeberDenHof OpenMenusExpanded -> "/ueber-den-hof"
  | UeberDenHof AllMenusExpanded -> "/ueber-den-hof?expand-all=1"
  | Lageplan -> "/lageplan"
  | Administration -> "/administration"

let toString = function
  | Aktivitaeten -> "Aktivitäten"
  | UeberDenHof OpenMenusExpanded
  | UeberDenHof AllMenusExpanded -> "Über den Hof"
  | Lageplan -> "Lageplan"
  | Administration -> "Administration"
