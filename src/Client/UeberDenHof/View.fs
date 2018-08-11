module UeberDenHof.View

open Types
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome

let root model dispatch =
  let h3 text =
    Heading.h3 [ Heading.Is5 ] [ str text ]

  let image src =
    Image.image [ ] [ img [ Src src ] ]

  let openMenuButton menuItem =
    let (cmd, stateClass) =
      if Set.contains menuItem model.OpenMenus then
        (CloseMenu menuItem, "is-expanded")
      else (OpenMenu menuItem, "is-collapsed")
    Button.a
      [ Button.Color Color.IsWhite
        Button.Modifiers [ Modifier.IsPulledRight ]
        Button.OnClick (fun _evt -> dispatch cmd)
        Button.CustomClass stateClass ]
      [ Icon.faIcon [ ]
          [ Fa.icon Fa.I.AngleRight; Fa.fa2x ] ]

  let animalBox item title collapsableContent picture =
    [ div [ ClassName Modifier.Classes.Helpers.IsClearfix ]
        [ openMenuButton item
          Heading.h3 [ Heading.Is5 ] [ str title ] ]
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
          [ Tile.child [ Tile.CustomClass Box.Classes.Container ]
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
                (image (importAll "../../../images/tiere/esel.jpg")))
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              (animalBox
                Ponys
                "Laika & Luna"
                [ p []
                    [ str "Unser Pony Laika ist 2017 zu uns auf den Hof gekommen. Sie ist mittlerweile 10 Jahre alt und eine sehr angenehme und ruhige Stute."
                      br []
                      str "Im April 2018 überraschte sie uns mit ihrem Fohlen \"Luna\"." ]
                  p []
                    [ str "Bezüglich dem Fressen gilt das gleiche wie bei den Eseln." ] ]
                (image (importAll "../../../images/tiere/ponys.jpg")))
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              (animalBox
                Kuehe
                "Josephine & Rosi"
                [ p []
                    [ str "Unsere beiden Zwergzeburinder kamen ebenfalls 2017 zu uns auf dem Hof."
                      br []
                      str "Die Kühe hatten beim Vorbesitzer sehr wenig Menschenkontakt, deshalb sind sie leider immer noch etwas schreckhaft." ] ]
                (image (importAll "../../../images/tiere/kuehe.jpg"))) ]
        Tile.parent [ Tile.IsVertical ]
          [ Tile.child [ Tile.CustomClass Box.Classes.Container ]
              (animalBox
                Hasen
                "Minki, Quetschi und Familie"
                [ p []
                    [ str "Unsere Hasen leben ebenfalls seit 2017 auf unserem Hof. Die beiden Eltern sorgten innerhalb dieser Zeit schon vier mal für Nachwuchs. Die Hasen werden gerne von den Kindern gefüttert, gestreichelt und beobachtet." ] ]
                (image (importAll "../../../images/tiere/hasen.jpg")))
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              (animalBox
                Huehner
                "Hahn & Hühner"
                [ p []
                    [ str "Unsere kleine Hühnerschar besteht aus einem stolzen Hahn und 13 Hühner. Sie lieben es sich unter den Sträuchern zu verkriechen und sich in der Erde zu wälzen." ] ]
                (image (importAll "../../../images/tiere/huehner.jpg")))
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              (animalBox
                Puma
                "Puma"
                [ p []
                    [ str "Unser Wachhund Puma lebt seit 2015 auf unserem Hof. Puma ist ein sehr netter Kerl. Er liebt Kinder über alles und tut keiner Mücke etwas zu Leide. Puma liebt es im Garten (oder in der Sandkiste) zu spielen." ] ]
                (image (importAll "../../../images/tiere/hund.jpg")))
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              (animalBox
                Maxi
                "Maxi"
                [ p []
                    [ str "Unser Maxi ist 2011 am Enserhof geboren. Er ist ein unkomplizierter, liebevoller und treuer Wegbegleiter." ] ]
                (image (importAll "../../../images/tiere/maxi.jpg"))) ] ] ]
