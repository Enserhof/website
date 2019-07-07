module Lageplan.View

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Fulma

// https://fettblog.eu/blog/2013/06/16/preserving-aspect-ratio-for-embedded-iframes/
let aspectRatio percent element =
  let elementStyle = [
    Position PositionOptions.Absolute
    Width "100%"
    Height "100%"
    Left "0"
    Top "0"
  ]
  div
    [ Style
        [ Position PositionOptions.Relative
          Width "100%"
          Height "0"
          PaddingBottom (sprintf "%d%%" (1. / percent * 100. |> int)) ] ]
    [ element elementStyle ]

let root =
    [ Heading.h1 [ Heading.Is3 ] [ str "Lageplan" ]
      aspectRatio (16./9.) (fun styles ->
        iframe
          [ Src "https://www.google.com/maps/embed?pb=!1m17!1m8!1m3!1d753.3551123114898!2d13.7883404!3d47.9512645!3m2!1i1024!2i768!4f13.1!4m6!3e0!4m3!3m2!1d47.9511462!2d13.788469699999998!4m0!5e1!3m2!1sen!2sat!4v1533618732198"
            FrameBorder "0"
            Style (styles @ [ Border "0" ])
            !!("allowFullScreen", ()) ]
          [ ] ) ]