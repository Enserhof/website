module Aktivitaeten.View

open Fable.Core.JsInterop
open Fable.FontAwesome
open Fable.MomentJs
open Fable.React
open Fable.React.Props
open Fulma
open System
open Types

let root model =
  let formatStallzeit = function
    | Timestamp dateTime ->
      let momentTime = moment.Invoke dateTime
      momentTime.format "dd, DD. MMMM YYYY, HH:mm \\U\\h\\r"
    | InfoText v -> v

  let stallzeitenContent =
    match model.Stallzeiten with
    | Loading ->
      [ str "Nächste Stallzeit:"
        Icon.icon [ ]
          [ Fa.i [ Fa.Solid.Spinner; Fa.Spin ] [] ] ]
    | Loaded times ->
        times
        |> List.filter (function
          | Timestamp v -> v.Date >= DateTime.Today
          | InfoText _ -> true)
        |> List.sortBy (function
          | Timestamp v -> v.Ticks
          | InfoText _ -> DateTime.MinValue.Ticks - 1L // Infos come first
        )
        |> function
        | [] ->
          [ str "Nächste Stallzeit: "
            b [] [ str "Wird noch bekannt gegeben" ] ]
        | [ time ] ->
          [ str "Nächste Stallzeit: "
            b [] [ str (formatStallzeit time) ] ]
        | times ->
          [ str "Nächste Stallzeiten: "
            ul []
              [ for time in times ->
                li []
                  [ b [] [ str (formatStallzeit time) ] ] ] ]
    | LoadError (HttpError _e) ->
      [ str "Fehler beim Laden der nächsten Stallzeiten. Bitte auf der Tafel, die vor unserem Hoftor steht, ablesen." ]

  [ Heading.h1 [ Heading.Is3 ]
      [ str "Aktivitäten" ]
    Heading.h2 [ Heading.Is4 ]
      [ str "Stallarbeit erledigen" ]
    Content.content []
      [ p []
          [ str "Ihr habt ab nun wie im letzten Jahr die Möglichkeit, mit uns in den Stall zu gehen."
            br []
            str "Wir reinigen gemeinsam die Koppel und die Ställe, pflücken Futter für die Hasen, heben gemeinsam frische Eier ab und füttern die Esel und Ponys mit Heu und Stroh."
            br []
            str "Das Stallgehen dauert ca. eine Stunde und findet bei jeder Witterung statt."
            br []
            str "Wir freuen uns, wenn ihr einfach mal vorbei schaut." ]
        span [] stallzeitenContent
        Image.image
          [ Image.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Left) ] ]
          [ img [ Src (importAll "../../images/stallarbeit.jpg")?``default``; Style [ MaxWidth "640px" ] ] ] ] ]
