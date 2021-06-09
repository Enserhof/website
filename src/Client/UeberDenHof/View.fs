module UeberDenHof.View

open Types
open Fable.Core.JsInterop
open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma

let root model dispatch =
  let h3 text =
    Heading.h3 [ Heading.Is5; Heading.Props [ Style [ LineHeight "36px" ] ] ] [ str text ]

  let image importedImage =
    Image.image [ ] [ img [ Src importedImage?``default`` ] ]

  let openMenuButton menuItem =
    let (cmd, stateClass) =
      if Set.contains menuItem model.OpenMenus
      then (CloseMenu menuItem, "is-expanded")
      else (OpenMenu menuItem, "is-collapsed")
    Button.a
      [ Button.Color IsWhite
        Button.Modifiers [ Modifier.IsPulledRight ]
        Button.OnClick (fun _evt -> dispatch cmd)
        Button.CustomClass stateClass ]
      [ Icon.icon [ ]
          [ Fa.i [ Fa.Solid.AngleRight; Fa.Size Fa.Fa2x ] [] ] ]

  let animalBox item title collapsableContent picture =
    [ Text.div [ Modifiers [ Modifier.IsClearfix ] ]
        [ openMenuButton item
          h3 title ]
      Content.content [ Content.CustomClass (if Set.contains item model.OpenMenus then "is-open" else "is-closed") ]
        collapsableContent
      picture ]

  [ Heading.h1 [ Heading.Is3 ]
      [ str "Über den Hof" ]
    Content.content []
      [ str "Der Enserhof ist seit vier Generationen ein Familienbetrieb."
        br []
        str "Früher wurde er vor allem zu Selbstversorgungszwecken genützt."
        br []
        str "Bis 1995 wurden hauptsächlich Kühe zur Milchproduktion gehalten."
        br []
        str "Zwischen 1995 und 2014 war der Betrieb dann eine reine Schweinezucht und -mast."
        br []
        str "Wir (Sylvia und Johannes) sind seit 2011 auf dem Hof."
        br []
        str "2012 sind die beiden Esel Lilly und Paula als Ausgleich vom Arbeitsalltag zu uns gekommen."
        br []
        str "2016 ist der Vierkanthof durch die Entfernung von zwei Seiten geöffnet worden und bietet seit dem Platz für unseren Garten." ]
    Heading.h2 [ Heading.Is4 ]
      [ str "Tiere" ]
    Tile.ancestor [ ]
      [ Tile.parent [ Tile.IsVertical; Tile.Size Tile.Is8 ]
          [ Tile.child [ Tile.CustomClass "box" ]
              (animalBox
                Esel
                "Lilly & Paula"
                [ p []
                    [ str "Unsere zwei Esel waren unsere ersten Begleiter. Sie kamen im Alter von ein (Lilly) und zwei (Paula) Jahren zu uns."
                      br []
                      str "Die beiden lieben sich gegenseitig heiß und es wäre unmöglich, sie zu trennen."
                      br []
                      str "Wenn wir mit ihnen fort gehen, dann zu zweit. Paula ist die Leitstute und eine starke Fresserin - sie vertreibt alle Tiere unserer Herde."
                      br []
                      str "Lilly ist diesbezüglich etwas entspannter. Sie ist aber beim Spazierengehen vorne auf." ]
                  p []
                    [ str "Unsere Esel bekommen von uns Heu, Stroh, einen Leckstein und etwas Gras zum Fressen."
                      br []
                      str "Brot und Obst haben zu viel Kohlenhydrate – das vertragen unsere Esel nicht!"
                      br []
                      str "Deshalb die beiden bitte nicht mehr füttern. Danke!" ] ]
                (image (importAll "../../images/tiere/esel.jpg")))
            Tile.child [ Tile.CustomClass "box" ]
              (animalBox
                Ponys
                "Laika & Luna"
                [ p []
                    [ str "Unser Pony Laika ist 2017 zu uns auf den Hof gekommen. Sie ist mittlerweile 10 Jahre alt und eine sehr angenehme und ruhige Stute."
                      br []
                      str "Im April 2018 überraschte sie uns mit ihrem Fohlen \"Luna\"." ]
                  p []
                    [ str "Bezüglich dem Fressen gilt das gleiche wie bei den Eseln." ] ]
                (image (importAll "../../images/tiere/ponys.jpg")))
            Tile.child [ Tile.CustomClass "box" ]
              (animalBox
                Huehner
                "Hahn & Hühner"
                [ p []
                    [ str "Unsere kleine Hühnerschar besteht aus einem stolzen Hahn und 13 Hühner. Sie lieben es sich unter den Sträuchern zu verkriechen und sich in der Erde zu wälzen." ] ]
                (image (importAll "../../images/tiere/huehner.jpg")))
          ]
        Tile.parent [ Tile.IsVertical ]
          [ Tile.child [ Tile.CustomClass "box" ]
              (animalBox
                Hasen
                "Minki, Quetschi und Familie"
                [ p []
                    [ str "Unsere Hasen leben ebenfalls seit 2017 auf unserem Hof. Die beiden Eltern sorgten innerhalb dieser Zeit schon vier mal für Nachwuchs. Die Hasen werden gerne von den Kindern gefüttert, gestreichelt und beobachtet." ] ]
                (image (importAll "../../images/tiere/hasen.jpg")))
            Tile.child [ Tile.CustomClass "box" ]
              (animalBox
                Puma
                "Puma"
                [ p []
                    [ str "Unser Wachhund Puma lebt seit 2015 auf unserem Hof. Puma ist ein sehr netter Kerl. Er liebt Kinder über alles und tut keiner Mücke etwas zu Leide. Puma liebt es im Garten (oder in der Sandkiste) zu spielen." ] ]
                (image (importAll "../../images/tiere/hund.jpg")))
            Tile.child [ Tile.CustomClass "box" ]
              (animalBox
                Maxi
                "Maxi"
                [ p []
                    [ str "Unser Maxi ist 2011 am Enserhof geboren. Er ist ein unkomplizierter, liebevoller und treuer Wegbegleiter." ] ]
                (image (importAll "../../images/tiere/maxi.jpg")))
            Tile.child [ Tile.CustomClass "box" ]
              (animalBox
                Lilo
                "Lilo"
                [ p []
                    [ str "Lilo ist uns im Herbst 2018 in einer stürmigen Nacht zugelaufen. Wir versuchten zwar, den Besitzer ausfindig zu machen, insgeheim hofften wir aber von Anfang an, dass Lilo bei uns bleiben kann. Sie ist eine äußerst zutrauliche Katze, will immer voll dabei sein und folgt oft auf Schritt und Tritt. Bei der Mäusejagd unterstützt sie Maxi tatkräftig und sehr erfolgreich." ] ]
                (image (importAll "../../images/tiere/lilo.jpg")))
          ]
      ]
  ]
