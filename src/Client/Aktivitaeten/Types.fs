module Aktivitaeten.Types

open System

type LoadStallzeitenError =
    | HttpError of exn

type Stallzeiten =
    | Loading
    | Loaded of DateTime list
    | LoadError of LoadStallzeitenError

type Model = {
    Stallzeiten: Stallzeiten
}

type Msg =
  | LoadStallzeitenSuccess of DateTime list
  | LoadStallzeitenError of LoadStallzeitenError
