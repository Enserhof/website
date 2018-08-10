module Global

open Microsoft.FSharp.Reflection

Fable.Helpers.Moment.moment.locale "de" |> ignore

type Page =
  | Aktivitaeten
  | UeberDenHof
  | Lageplan
  | Administration

let allPages =
  let cases = FSharpType.GetUnionCases typeof<Page>
  [ for c in cases -> FSharpValue.MakeUnion(c, [| |]) :?> Page ]

let publicPages =
  allPages
  |> List.except [ Administration ]

let toHash = function
  | Aktivitaeten -> "#aktivitaeten"
  | UeberDenHof -> "#ueber-den-hof"
  | Lageplan -> "#lageplan"
  | Administration -> "#administration"

let toUrl = toHash >> fun s -> s.Replace("#", "/")

let toString = function
  | Aktivitaeten -> "Aktivitäten"
  | UeberDenHof -> "Über den Hof"
  | Lageplan -> "Lageplan"
  | Administration -> "Administration"
