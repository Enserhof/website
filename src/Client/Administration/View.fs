module Administration.View

open System
open Types
open Fulma
open Fulma.FontAwesome
open Fable.Core
open Fable.Helpers.Moment
open Fable.Helpers.React
open Fable.Helpers.React.Props

let root model dispatch =
  let timeInput fieldId time =
    let (value, color) =
      match time with
      | Valid value ->
        let momentTime = moment.Invoke value
        momentTime.format "YYYY-MM-DDTHH:mm", Color.IsSuccess
      | Invalid value -> value, Color.IsDanger

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
            Icon.faIcon [ Icon.IsLeft ]
              [ Fa.icon Fa.I.Calendar ] ]
        Control.p []
          [ Button.button
              [ Button.Color Color.IsDanger
                Button.OnClick (fun _evt -> dispatch (RemoveStallzeit fieldId)) ]
              [ Icon.faIcon []
                  [ Fa.icon Fa.I.Trash ] ] ] ]

  let infoTextInput fieldId text =
    Field.div [ Field.HasAddons ]
      [ Control.p [ Control.HasIconLeft ]
          [ Input.text
              [ Input.Value text
                Input.Color (if String.IsNullOrWhiteSpace text then Color.IsDanger else Color.IsSuccess)
                Input.OnChange (fun evt -> dispatch (UpdateStallzeit (fieldId, InfoText evt.Value))) ]
            Icon.faIcon [ Icon.IsLeft ]
              [ Fa.icon Fa.I.Info ] ]
        Control.p []
          [ Button.button
              [ Button.Color Color.IsDanger
                Button.OnClick (fun _evt -> dispatch (RemoveStallzeit fieldId)) ]
              [ Icon.faIcon []
                  [ Fa.icon Fa.I.Trash ] ] ] ]

  let stallzeitInput stallzeit =
    match stallzeit.Value with
    | Timestamp v -> timeInput stallzeit.Id v
    | InfoText v -> infoTextInput stallzeit.Id v

  let existingStallzeiten =
    match model.RemoteStallzeiten with
    | NotLoaded ->
      [ Field.div []
          [ Control.p [ Control.HasIconLeft ]
              [ Input.text
                  [ Input.Placeholder "GitHub Access Token"
                    Input.OnChange (fun evt -> dispatch (UpdateGitHubAccessToken evt.Value)) ]
                Icon.faIcon [ Icon.IsLeft ]
                  [ Fa.icon Fa.I.Lock ] ] ]
        Field.div []
          [ Control.p []
              [ Button.button
                  [ Button.Color Color.IsSuccess
                    Button.OnClick (fun _evt -> dispatch Login) ]
                  [ str "Login" ] ] ] ]
    | Loading ->
      [ Icon.faIcon [ ]
          [ Fa.icon Fa.I.Spinner; Fa.spin ] ]
    | Loaded _ ->
        [
          yield! List.map stallzeitInput model.LocalStallzeiten
          yield Field.div [ Field.IsGrouped ]
            [ Control.p []
                [ Button.button
                    [ Button.Color Color.IsSuccess
                      Button.OnClick (fun _evt -> dispatch AddStallzeitTimestamp)
                      Button.Props [ Title "Zeitpunkt hinzufügen" ] ]
                    [ Icon.faIcon [] [ Fa.icon Fa.I.CalendarPlusO ] ] ]
              Control.p []
                [ Button.button
                    [ Button.Color Color.IsSuccess
                      Button.OnClick (fun _evt -> dispatch AddStallzeitInfoText)
                      Button.Props [ Title "Infotext hinzufügen" ] ]
                    [ Icon.faIcon [] [ Fa.icon Fa.I.FileText ] ] ]
              Control.p []
                [ Button.button
                    [ Button.Color Color.IsSuccess
                      Button.OnClick (fun _evt -> dispatch SaveStallzeiten) ]
                    [ Icon.faIcon [] [ Fa.icon Fa.I.Save ] ] ] ]
        ]
    | LoadError (LoadStallzeitenError.HttpError e) ->
      [ Notification.notification [ Notification.Color Color.IsDanger ] [ str (sprintf "Fehler beim Laden der Stallzeiten.") ] ]
    | LoadError (ParseError e) ->
      [ Notification.notification [ Notification.Color Color.IsDanger ] [ str (sprintf "Fehler beim Parsen der Stallzeiten.") ] ]

  [ yield Heading.h1 [ Heading.Is3 ] [ str "Administration" ]
    yield Heading.h2 [ Heading.Is4 ] [ str "Stallzeiten aktualisieren" ]
    yield! existingStallzeiten ]
