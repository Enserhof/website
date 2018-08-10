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

type StallzeitValue =
  | Invalid of string
  | Valid of DateTime

type LocalStallzeit = {
  Id: string
  Value: StallzeitValue
}

type LoadedStallzeiten = {
  Version: string
  FileUrl: string
  Stallzeiten: DateTime list
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
  | UpdateStallzeit of (string * string)
  | AddStallzeit
  | RemoveStallzeit of string
  | SaveStallzeiten
  | SaveStallzeitenSuccess of string
  | SaveStallzeitenError of SaveStallzeitenError
