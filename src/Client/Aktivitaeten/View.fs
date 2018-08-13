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
