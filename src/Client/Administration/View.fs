module Administration.View

open Types
open Fulma
open Fulma.FontAwesome
open Fable.Helpers.Moment
open Fable.Helpers.React

let root model dispatch =
  let timeInput time =
    let (value, color) =
      match time.Value with
      | Valid value ->
        let momentTime = moment.Invoke value
        momentTime.format "YYYY-MM-DDTHH:mm", Color.IsSuccess
      | Invalid value -> value, Color.IsDanger

    Field.div [ Field.HasAddons ]
      [ Control.p [ Control.HasIconLeft ]
          [ Input.datetimeLocal
              [ Input.Value value
                Input.Color color
                Input.OnChange (fun evt -> dispatch (UpdateStallzeit (time.Id, evt.Value))) ]
            Icon.faIcon [ Icon.IsLeft ]
              [ Fa.icon Fa.I.Calendar ] ]
        Control.p []
          [ Button.button
              [ Button.Color Color.IsDanger
                Button.OnClick (fun _evt -> dispatch (RemoveStallzeit time.Id)) ]
              [ Icon.faIcon []
                  [ Fa.icon Fa.I.Trash ] ] ] ]

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
          yield! List.map timeInput model.LocalStallzeiten
          yield Field.div [ Field.IsGrouped ]
            [ Control.p []
                [ Button.button
                    [ Button.Color Color.IsSuccess
                      Button.OnClick (fun _evt -> dispatch AddStallzeit) ]
                    [ Icon.faIcon [] [ Fa.icon Fa.I.Plus ] ] ]
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
