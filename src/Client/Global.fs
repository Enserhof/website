module Global

Fable.Helpers.Moment.moment.locale "de" |> ignore

type UeberDenHofMenuState =
  | OpenMenusExpanded
  | AllMenusExpanded

type Page =
  | Aktivitaeten
  | UeberDenHof of UeberDenHofMenuState
  | Lageplan
  | Administration

let allPages =
  [
    Aktivitaeten
    UeberDenHof OpenMenusExpanded
    Lageplan
    Administration
  ]

let publicPages =
  allPages
  |> List.except [ Administration ]

let toHash page =
  match page with
  | Aktivitaeten -> "#aktivitaeten"
  | UeberDenHof OpenMenusExpanded -> "#ueber-den-hof"
  | UeberDenHof AllMenusExpanded -> "#ueber-den-hof/expand-all"
  | Lageplan -> "#lageplan"
  | Administration -> "#administration"

let toUrl = toHash >> fun s -> s.Replace("#", "/")

let toString = function
  | Aktivitaeten -> "Aktivitäten"
  | UeberDenHof OpenMenusExpanded -> "Über den Hof"
  | UeberDenHof AllMenusExpanded -> "Über den Hof"
  | Lageplan -> "Lageplan"
  | Administration -> "Administration"
