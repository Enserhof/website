module Startseite.View

open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop
open Fulma

let root =
  [ Content.content []
      [ Notification.notification [ Notification.Color IsPrimary; Notification.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ] [
          str "Du liebst Tiere und genießt es, sie zu beobachten und Zeit mit ihnen zu verbringen? Dann bist du bei uns genau richtig!"
        ]
        p [] [
            img [ Src (importAll "../../images/startseite/hof.jpg")?``default``]
        ]
        p [] [ str "Wir, Johannes und Sylvia, leben mit unseren zwei Kindern auf dem Enserhof im wunderschönen Ehrendorf bei Ohlsdorf - ca. 10 Autominuten von Gmunden entfernt." ]
        p [] [
          str "Im Laufe der Jahre haben sich immer mehr verschiedene Tiere auf dem Hof eingelebt. "
          str "Es ist uns eine Herzensangelegenheit, dass unsere Tiere ihren grundlegenden Bedürfnissen bei uns auf dem Hof nachkommen können, dass sie mit biologischem Futter gefüttert werden und dass sie genug Platz zum Bewegen haben."
        ]
      ]
  ]
