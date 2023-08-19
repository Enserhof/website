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
        p [] [ str "Wir, Johannes und Sylvia, leben mit unseren drei Kindern auf dem Enserhof in Ehrendorf bei Ohlsdorf - ca. 5 Autominuten von Gmunden entfernt." ]
        p [] [
          str "Im Laufe der Jahre haben sich immer mehr verschiedene Tiere auf dem Hof eingelebt. "
          str "Es ist uns eine Herzensangelegenheit, dass unsere Tiere einen nahen Kontakt zu Menschen haben, dass sie ihren grundlegenden Bedürfnissen nachkommen können und dass sie bei uns auf dem Hof genug Platz zum Bewegen haben."
        ]
      ]
  ]
