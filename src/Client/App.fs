module App.View

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Fable.Core.JsInterop
open App.State
open App.Types
open Global
open Fable.React
open Fable.React.Props
open Fulma

importAll "../../sass/main.sass"

let menuItem page currentPage =
  Navbar.Item.a
    [ Navbar.Item.IsActive (page = currentPage)
      Navbar.Item.Props [ Href (toHash page) ] ]
    [ str (toString page) ]

let menu pages currentPage =
  Navbar.navbar [ Navbar.Color IsLight ]
    [ for page in pages -> menuItem page currentPage ]

let root model dispatch =
  let pageHtml =
    function
    | Aktivitaeten -> Aktivitaeten.View.root model.Aktivitaeten
    | UeberDenHof _menuState -> UeberDenHof.View.root model.UeberDenHof (UeberDenHofMsg >> dispatch)
    | Lageplan -> Lageplan.View.root
    | Administration -> Administration.View.root model.Administration (AdministrationMsg >> dispatch)

  let hasGitHubAccessToken = model.Administration.GitHubAccessToken <> ""
  let currentPageIsNotPublic = publicPages |> List.contains model.CurrentPage |> not
  let pages =
    if hasGitHubAccessToken || currentPageIsNotPublic
    then allPages
    else publicPages

  div []
    [ Hero.hero [ ]
        [ Hero.body [ ]
            [ Container.container [ Container.IsFluid ]
                [ Heading.h2
                    [ Heading.Is4
                      Heading.IsSubtitle
                      Heading.Modifiers [ Modifier.TextColor IsLight ] ]
                    [ str "Herzlich Willkommen am" ]
                  Heading.h1 [ Heading.Modifiers [ Modifier.TextColor IsWhite ] ]
                    [ str "Enserhof z'Ehrndorf" ] ] ] ]
      menu pages model.CurrentPage
      Section.section [ Section.CustomClass "main-section" ]
        [ Container.container [ ]
            (pageHtml model.CurrentPage) ]
    ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR // Must be last Elmish.* open declaration (see https://elmish.github.io/hmr/#Usage)

// App
Program.mkProgram init update root
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withDebugger
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
|> Program.run

match Browser.Navigator.navigator.serviceWorker with
| Some serviceWorker ->
  Browser.Dom.window.addEventListener("load", fun _evt ->
    promise {
      try
        let! registration = serviceWorker.register "sw.js"
        Browser.Dom.console.log("Service Worker is registered", registration)
      with e ->
        Browser.Dom.console.error("ServiceWorker registration failed", e)
    }
    |> Promise.start
  )
| None -> ()
