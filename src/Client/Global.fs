module Global

open Fable.MomentJs

moment.locale "de" |> ignore

type UeberDenHofMenuState =
  | OpenMenusExpanded
  | AllMenusExpanded

type Page =
  | Startseite
  | Angebote
  | UeberDenHof of UeberDenHofMenuState
  | Lageplan
  | Administration

let toUrl page =
  match page with
  | Startseite -> "/"
  | Angebote -> "/angebote"
  | UeberDenHof OpenMenusExpanded -> "/ueber-den-hof"
  | UeberDenHof AllMenusExpanded -> "/ueber-den-hof?expand-all=1"
  | Lageplan -> "/lageplan"
  | Administration -> "/administration"

let toString = function
  | Startseite -> "Startseite"
  | Angebote -> "Angebote"
  | UeberDenHof OpenMenusExpanded
  | UeberDenHof AllMenusExpanded -> "Über den Hof"
  | Lageplan -> "Lageplan"
  | Administration -> "Administration"
