module App.Types

open Global

type Message =
  | ShowPage of Page
  | AngeboteMsg of Angebote.Types.Msg
  | UeberDenHofMsg of UeberDenHof.Types.Msg
  | AdministrationMsg of Administration.Types.Msg

type Model = {
  CurrentPage: Page
  Angebote: Angebote.Types.Model
  UeberDenHof: UeberDenHof.Types.Model
  Administration: Administration.Types.Model
}
