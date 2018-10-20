module Aktivitaeten.View

open System
open Types
open Fable.Core.JsInterop
open Fable.Helpers.Moment
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome

let root model =
  let formatTime (dateTime: DateTime) =
    let momentTime = moment.Invoke dateTime
    momentTime.format "dd, DD. MMMM YYYY, HH:mm \\U\\h\\r"

  let stallzeitenContent =
    match model.Stallzeiten with
    | Loading ->
      [ str "Nächste Stallzeit:"
        Icon.faIcon [ ]
          [ Fa.icon Fa.I.Spinner; Fa.spin ] ]
    | Loaded times ->
        times
        |> List.filter (fun d -> d.Date >= DateTime.Today)
        |> List.sort
        |> function
        | [] ->
          [ str "Nächste Stallzeit: "
            b [] [ str "Wird noch bekannt gegeben" ] ]
        | [ time ] ->
          [ str "Nächste Stallzeit: "
            b [] [ str (formatTime time) ] ]
        | times ->
          [ str "Nächste Stallzeiten: "
            ul []
              [ for time in times ->
                li []
                  [ b [] [ str (formatTime time) ] ] ] ]
    | LoadError (HttpError _e) ->
      [ str "Fehler beim Laden der nächsten Stallzeiten. Bitte auf der Tafel, die vor unserem Hoftor steht, ablesen." ]

  [ Heading.h1 [ Heading.Is3 ]
      [ str "Aktivitäten" ]
    Heading.h2 [ Heading.Is4 ]
      [ str "Rindfleisch Ab-Hof-Verkauf" ]
    Content.content []
      [ p [] [ span [] [ str "Liebe Leute!" ] ]
        p [] [ span [] [ str "Wir haben am Mittwoch, 24. Oktober 2018, von 17-18 Uhr frisches Rindfleisch von unseren zwei Kühen zu verkaufen." ] ]
        Columns.columns [ ]
          [ Column.column [ Column.Width (Screen.All, Column.IsNarrow) ]
              [ ul []
                  [
                    let items = [
                      "Rindsschnitzel", "16"
                      "Braten", "16"
                      "Kochfleisch", "11"
                      "Gulasch", "11"
                      "Rohwürstl", "20"
                      "Gebratene Wurst", "15"
                    ]
                    for (name, price) in items ->
                      li [] [ str (sprintf "%s: %s € / kg" name price) ] ] ]
            Column.column [ Column.Width (Screen.All, Column.IsNarrow) ]
              [ div [ Style [ MarginTop "1em" ] ]
                  [ img [ Src (importAll "../../images/tiere/kuehe.jpg"); Style [ Height "150px" ] ] ] ] ]
        p [] [ span [] [ str "Wir bitten um Rückmeldung bis Dienstag Abend, was und wie portioniert ihr es braucht. Bsp.: 6 x 5 Rindsschnitzel." ] ] ]
    Heading.h2 [ Heading.Is4 ]
      [ str "Stallarbeit erledigen" ]
    Content.content []
      [ p []
          [ str "Ihr habt ab nun die Möglichkeit, mit uns in den Stall zu gehen."
            br []
            str "Wir reinigen gemeinsam die Koppel und die Ställe, pfücken Futter für die Hasen, heben gemeinsam frische Eier ab und füttern die Esel, Kühe und Ponys mit Heu."
            br []
            str "Das Stallgehen dauert ca. eine Stunde und findet bei jeder Witterung statt."
            br []
            str "Wir freuen uns, wenn ihr einfach mal vorbei schaut." ]
        span [] stallzeitenContent
        Image.image
          [ Image.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Left) ] ]
          [ img [ Src (importAll "../../images/stallarbeit.jpg"); Style [ MaxWidth "640px" ] ] ] ] ]
