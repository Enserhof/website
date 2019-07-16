module Administration.View

open Fable.Core
open Fable.FontAwesome
open Fable.Helpers.Moment
open Fable.React
open Fable.React.Props
open Fulma
open System
open Types

let root model dispatch =
  let timeInput fieldId time =
    let (value, color) =
      match time with
      | Valid value ->
        let momentTime = moment.Invoke value
        momentTime.format "YYYY-MM-DDTHH:mm", IsSuccess
      | Invalid value -> value, IsDanger

    Field.div [ Field.HasAddons ]
      [ Control.p [ Control.HasIconLeft ]
          [ Input.datetimeLocal
              [ Input.Value value
                Input.Color color
                Input.OnChange (fun evt ->
                  let value =
                    match moment.Invoke(evt.Value, U3.Case1 "YYYY-MM-DDTHH:mm", true) with
                    | value when value.isValid() -> Valid (value.toDate())
                    | _ -> Invalid evt.Value
                    |> Timestamp
                  dispatch (UpdateStallzeit (fieldId, value))) ]
            Icon.icon [ Icon.IsLeft ]
              [ Fa.i [ Fa.Solid.Calendar ] [] ] ]
        Control.p []
          [ Button.button
              [ Button.Color IsDanger
                Button.OnClick (fun _evt -> dispatch (RemoveStallzeit fieldId)) ]
              [ Icon.icon []
                  [ Fa.i [ Fa.Solid.Trash ] [] ] ] ] ]

  let infoTextInput fieldId text =
    Field.div [ Field.HasAddons ]
      [ Control.p [ Control.HasIconLeft ]
          [ Input.text
              [ Input.Value text
                Input.Color (if String.IsNullOrWhiteSpace text then IsDanger else IsSuccess)
                Input.OnChange (fun evt -> dispatch (UpdateStallzeit (fieldId, InfoText evt.Value))) ]
            Icon.icon [ Icon.IsLeft ]
              [ Fa.i [ Fa.Solid.Info ] [] ] ]
        Control.p []
          [ Button.button
              [ Button.Color IsDanger
                Button.OnClick (fun _evt -> dispatch (RemoveStallzeit fieldId)) ]
              [ Icon.icon []
                  [ Fa.i [ Fa.Solid.Trash ] [] ] ] ] ]

  let stallzeitInput stallzeit =
    match stallzeit.Value with
    | Timestamp v -> timeInput stallzeit.Id v
    | InfoText v -> infoTextInput stallzeit.Id v

  let stallzeitenForm =
    [
      yield! List.map stallzeitInput model.LocalStallzeiten
      yield Field.div [ Field.IsGrouped ]
        [ Control.p []
            [ Button.button
                [ Button.Color IsSuccess
                  Button.OnClick (fun _evt -> dispatch AddStallzeitTimestamp)
                  Button.Props [ Title "Zeitpunkt hinzufügen" ] ]
                [ Icon.icon [] [ Fa.i [ Fa.Solid.CalendarPlus ] [] ] ] ]
          Control.p []
            [ Button.button
                [ Button.Color IsSuccess
                  Button.OnClick (fun _evt -> dispatch AddStallzeitInfoText)
                  Button.Props [ Title "Infotext hinzufügen" ] ]
                [ Icon.icon [] [ Fa.i [ Fa.Solid.File ] [] ] ] ]
          Control.p []
            [ Button.button
                [ Button.Color IsSuccess
                  Button.OnClick (fun _evt -> dispatch SaveStallzeiten) ]
                [ Icon.icon [] [ Fa.i [ Fa.Solid.Save ] [] ] ] ] ]
    ]

  let existingStallzeiten =
    match model.RemoteStallzeiten with
    | NotLoaded ->
      [ Field.div []
          [ Control.p [ Control.HasIconLeft ]
              [ Input.text
                  [ Input.Placeholder "GitHub Access Token"
                    Input.OnChange (fun evt -> dispatch (UpdateGitHubAccessToken evt.Value)) ]
                Icon.icon [ Icon.IsLeft ]
                  [ Fa.i [ Fa.Solid.Lock ] [] ] ] ]
        Field.div []
          [ Control.p []
              [ Button.button
                  [ Button.Color IsSuccess
                    Button.OnClick (fun _evt -> dispatch Login) ]
                  [ str "Login" ] ] ] ]
    | Loading ->
      [ Icon.icon [ ]
          [ Fa.i [ Fa.Solid.Spinner; Fa.Spin ] [] ] ]
    | Loaded _ ->
        stallzeitenForm
    | LoadError (LoadStallzeitenError.HttpError e) ->
      [ yield Notification.notification [ Notification.Color IsDanger ] [ str (sprintf "Fehler beim Laden der Stallzeiten.") ]
        yield! stallzeitenForm ]
    | LoadError (ParseError e) ->
      [ yield Notification.notification [ Notification.Color IsDanger ] [ str (sprintf "Fehler beim Parsen der Stallzeiten.") ]
        yield! stallzeitenForm ]

  [ yield Heading.h1 [ Heading.Is3 ] [ str "Administration" ]
    yield Heading.h2 [ Heading.Is4 ] [ str "Stallzeiten aktualisieren" ]
    yield! existingStallzeiten ]
