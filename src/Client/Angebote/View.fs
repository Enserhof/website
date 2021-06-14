module Angebote.View

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

  let entry title content =
    Box.box' [] [
        Heading.h2 [ Heading.Is4 ] [ str title ]
        Content.content [] content
    ]

  [ Heading.h1 [ Heading.Is3 ] [ str "Angebote" ]
    entry "Kindergeburtstag" [
      p [] [
        str "Eine Geburtstagsparty der etwas anderen Art kannst du ab deinem 5. Geburtstag bei und mit uns feiern."
        str " Beim Geburtstagsfest wird unter anderem"
        ul [] [
          li [] [ str "ein gemeinsames Geburtstagslied für dich gesungen," ]
          li [] [ str "Bewegungsspiele gespielt," ]
          li [] [ str "Tiere gestreichelt und abgeschmust," ]
          li [] [ str "im Heuhaufen herumgesprungen, " ]
          li [] [ str "Pony geritten und" ]
          li [] [ str "ein Tierquiz gespielt." ]
        ]
      ]
      Heading.h3 [ Heading.Is5 ] [ str "Details" ]
      p [] [
        ul [] [
          li [] [ b [] [ str "Dauer:" ]; str " 2 ½ Stunden (z.B. von 14:00 Uhr bis 16:30 Uhr)" ]
          li [] [ b [] [ str "Kosten:" ]; str " 170 €" ]
          li [] [ b [] [ str "Anzahl der Kinder:" ]; str " max. 8 inkl. Geburtstagskind" ]
        ]
      ]
      p [] [
        str "Deine Mama oder dein Papa nehmen bitte deinen Lieblingskuchen mit, der beim gemeinsamen Feiern mit deinen Freunden gegessen wird."
        str " Wasser und Saft sowie Geschirr für den Kuchen werden von uns zur Verfügung gestellt."
      ]
      p [] [
        str "Die Anwesenheit eines Elternteils von dir ist erforderlich."
        str " Du und deine Freunde benötigen festes Schuhwerk sowie witterungsentsprechende Kleidung."
      ]
      p [] [
        str "Wir freuen uns auf dich und deine Freunde."
      ]
    ]

    entry "Ponyreiten" [
        p [] [ str "Die Möglichkeiten zum Ponyreiten richten sich hauptsächlich nach deinem Alter, deiner Erfahrung mit Ponys und Tieren im Allgemeinen und nach deinem Geschick." ]
        Heading.h3 [ Heading.Is5 ] [ str "Minireiter (2 - 3 Jahre, ca. 30 Minuten) - der erste Kontakt mit Ponys" ]
        p [] [
            str "Gemeinsam wird unser Pony Laika geputzt, um eine Verbindung zwischen dir und dem Pferd aufzubauen."
            str " Anschließend reinigen wir gemeinsam die Hufe und satteln das Pony."
            str " Jetzt geht es los."
            str " Während du dich gut am Haltegurt festhältst, reiten wir durch unseren Obstgarten."
            str " Nach ein paar Runden bringen wir das Pony auf die Koppel zurück, wo du es zum Abschluss mit einer Karotte belohnen darfst."
        ]
        p [] [ b [] [ str "Kosten: "]; str "12 €" ]

        Heading.h3 [ Heading.Is5 ] [ str "Kinderreiter (4 - 7 Jahre, ca. 45 Minuten)" ]
        p [] [
            str "Zu Beginn darfst du unser Pony putzen, um eine Verbindung mit ihm aufzubauen und Selbstbewusstsein im Umgang mit Pferden zu erlangen."
            str " Anschließend werden die Hufe kontrolliert und gereinigt und dann das Pony gesattelt."
            str " Je nach Erfahrung und Geschick verwenden wir entweder den Haltegurt oder unseren Westernsattel."
            str " Beim Kinderreiten bleiben wir auf unserer Hofwiese und machen Übungen am Pony."
        ]
        p [] [ b [] [ str "Kosten: "]; str "18 €" ]

        Heading.h3 [ Heading.Is5 ] [ str "Ausritt (ab 6 Jahren, ca. 1 - 1 ½ Stunden)" ]
        p [] [
            str "Nach dem gemeinsamen Putzen, Hufe reinigen und satteln machen wir uns auf den Weg zu unserem nahegelegenen Wald."
            str " Auf dich wartet viel Natur, eine wundervolle Kulisse und ein kleines Abenteuer durch den Wald."
        ]
        p [] [ b [] [ str "Kosten: "]; str "24 - 36 €" ]
    ]

    entry "Esel- oder Ziegenwanderung" [
        p [] [
            str "Ihr möchtet wandern, entschleunigen und dabei in tierischer Begleitung sein?"
            str " Dann entführt doch unsere Esel Paula und Lilly oder unsere Ziegen Lisi und Sissy zu einem Spaziergang."
        ]
        p [] [
            str " Der ruhige und langsame Gang der Esel wirkt besonders entspannend und macht eure Wanderung zu einer Wohltat für unruhige Geister."
            str " Die Ziegen sind dafür umso lebhafter und sorgen mit ihren Kletterkünsten und ihrer neugierigen Art immer für ein Schmunzeln."
        ]
        p [] [
            str " Bevor es losgeht, werden die Esel geputzt und ihre Hufe gereinigt. Die Ziegen sind sofort startklar."
            str " Dann versorgen wir euch noch mit ein paar nützlichen Hinweisen für den Umgang mit euren tierischen Begleitern und wünschen euch eine nette Zeit."
        ]
        p [] [
            str " Für die Wanderung sollten mindestens zwei Erwachsene dabei sein, denn sowohl die Esel als auch die Ziegen sind nicht gerne alleine und können auch mal streiken, wenn sie sich zu weit voneinander entfernen."
        ]
        p [] [
            b [] [ str "Kosten: "]
            ul [] [
                li [] [ str "Halbtageswanderung (bis 4 Stunden): 30 €" ]
                li [] [ str "Ganztageswanderung (nur mit den Eseln, bis 8 Stunden): 50 €" ]
            ]
        ]
    ]

    entry "Stallarbeit für Kinder" [
        p []
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
          [ img [ Src (importAll "../../images/stallarbeit.jpg")?``default``; Style [ MaxWidth "640px" ] ] ]
    ]

    entry "Eier von Freilandhühnern" [
        p [] [
            str "Unsere kleine Hühnerschar wird biologisch gefüttert und gehalten. Die Eier sind direkt bei uns erhältlich."
        ]
        p [] [
            b [] [ str "Kosten: "]
            ul [] [
                li [] [ str "Sechserpackung: 3 €" ]
                li [] [ str "Zehnerpackung: 5 €" ]
            ]
        ]
    ]
  ]
