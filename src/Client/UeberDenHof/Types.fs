module UeberDenHof.Types

type MenuItem =
  | Esel
  | Ponys
  | Kuehe
  | Hasen
  | Huehner
  | Puma
  | Maxi

type Model = {
    OpenMenus: Set<MenuItem>
}

type Msg =
  | OpenMenu of MenuItem
  | CloseMenu of MenuItem
