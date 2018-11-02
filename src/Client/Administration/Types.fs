module Administration.Types

open System

module GitHubApi =
  // see https://developer.github.com/v3/repos/contents/#get-contents
  type GetContentResponse = {
    url: string
    content: string
    sha: string
  }

  // see https://developer.github.com/v3/repos/contents/#update-a-file
  type SetContentRequest = {
    message: string
    content: string
    sha: string
    branch: string
  }

  // see https://developer.github.com/v3/repos/contents/#response-2
  type SetContentResponse = {
    content: GetContentResponse
  }

type LoadStallzeitenError =
  | HttpError of exn
  | ParseError of exn

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
  | SaveStallzeitenSuccess of string
  | SaveStallzeitenError of SaveStallzeitenError
