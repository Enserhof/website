module Aktivitaeten.Types

open System
open Thoth.Json

type LoadStallzeitenError =
    | HttpError of exn

type Stallzeit =
    | Timestamp of DateTime
    | InfoText of string

module Stallzeit =
    let decoder: Decoder<_> =
        Decode.oneOf [
            Decode.field "timestamp" Decode.datetime |> Decode.map Timestamp
            Decode.field "infotext" Decode.string |> Decode.map InfoText
        ]

    let encode v =
        match v with
        | Timestamp t -> Encode.object [ "timestamp", Encode.datetime t ]
        | InfoText t -> Encode.object [ "infotext", Encode.string t ]

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
