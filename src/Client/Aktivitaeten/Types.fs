module Aktivitaeten.Types

open System

type LoadStallzeitenError =
    | HttpError of exn

type Stallzeit =
    | Timestamp of DateTime
    | InfoText of string

type Stallzeiten =
    | Loading
    | Loaded of Stallzeit list
    | LoadError of LoadStallzeitenError

type Model = {
    Stallzeiten: Stallzeiten
}

type Msg =
  | LoadStallzeitenSuccess of Stallzeit list
  | LoadStallzeitenError of LoadStallzeitenError
