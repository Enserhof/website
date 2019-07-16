module Administration.Types

open System
open Thoth.Json

module GitHubApi =
  // see https://developer.github.com/v3/repos/contents/#get-contents
  type GetContentResponse = {
    Url: string
    Content: string
    Sha: string
  }

  module GetContentResponse =
    let decoder: Decoder<_> =
      Decode.object (fun get ->
        {
          Url = get.Required.Field "url" Decode.string
          Content = get.Required.Field "content" Decode.string
          Sha = get.Required.Field "sha" Decode.string
        }
      )

  // see https://developer.github.com/v3/repos/contents/#update-a-file
  type SetContentRequest = {
    Message: string
    Content: string
    Sha: string option
    Branch: string
  }

  module SetContentRequest =
    let encode v =
      Encode.object [
        yield "message", Encode.string v.Message
        yield "content", Encode.string v.Content
        match v.Sha with
        | Some sha ->
          yield "sha", Encode.string sha
        | None -> ()
        yield "branch", Encode.string v.Branch
      ]

  // see https://developer.github.com/v3/repos/contents/#response-2
  type SetContentResponse = {
    Sha: string
  }

  module SetContentResponse =
    let decoder: Decoder<_> =
      Decode.object (fun get ->
        {
          Sha = get.Required.At ["content"; "sha"] Decode.string
        }
      )

type LoadStallzeitenError =
  | HttpError of exn
  | ParseError of string

type SaveStallzeitenError =
  | HttpError of exn

type StallzeitTimestampValue =
  | Invalid of string
  | Valid of DateTime

type StallzeitValue =
  | Timestamp of StallzeitTimestampValue
  | InfoText of string

type LocalStallzeit = {
  Id: string
  Value: StallzeitValue
}

type LoadedStallzeiten = {
  Version: string
  FileUrl: string
  Stallzeiten: Aktivitaeten.Types.Stallzeit list
}

type Stallzeiten =
  | NotLoaded
  | Loading
  | Loaded of LoadedStallzeiten
  | LoadError of LoadStallzeitenError

type Model = {
  GitHubAccessToken: string
  RemoteStallzeiten: Stallzeiten
  LocalStallzeiten: LocalStallzeit list
}

type Msg =
  | LoadStallzeiten
  | LoadStallzeitenSuccess of GitHubApi.GetContentResponse
  | LoadStallzeitenError of LoadStallzeitenError
  | UpdateGitHubAccessToken of string
  | Login
  | UpdateStallzeit of (string * StallzeitValue)
  | AddStallzeitTimestamp
  | AddStallzeitInfoText
  | RemoveStallzeit of string
  | SaveStallzeiten
  | SaveStallzeitenSuccess of GitHubApi.SetContentResponse
  | SaveStallzeitenError of SaveStallzeitenError
