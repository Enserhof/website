module Lageplan.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma

let root =
    [ Heading.h1 [ Heading.Is3 ] [ str "Lageplan" ]
      iframe
        [ Src "https://www.google.com/maps/embed?pb=!1m17!1m8!1m3!1d753.3551123114898!2d13.7883404!3d47.9512645!3m2!1i1024!2i768!4f13.1!4m6!3e0!4m3!3m2!1d47.9511462!2d13.788469699999998!4m0!5e1!3m2!1sen!2sat!4v1533618732198"
          FrameBorder "0"
          Style [ Width "600px"; Height "450px"; Border "0" ]
          !!("allowFullScreen", ()) ]
        [ ] ]