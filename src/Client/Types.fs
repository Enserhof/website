module App.Types

open Global

type Message =
  | AktivitaetenMsg of Aktivitaeten.Types.Msg
  | UeberDenHofMsg of UeberDenHof.Types.Msg
  | AdministrationMsg of Administration.Types.Msg

type Model = {
  CurrentPage: Page
  Aktivitaeten: Aktivitaeten.Types.Model
  UeberDenHof: UeberDenHof.Types.Model
  Administration: Administration.Types.Model
}
