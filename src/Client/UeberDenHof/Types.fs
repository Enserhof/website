module UeberDenHof.Types

open Microsoft.FSharp.Reflection

type MenuItem =
  | Esel
  | Ponys
  | Kuehe
  | Hasen
  | Huehner
  | Puma
  | Maxi

let allMenuItems =
  let cases = FSharpType.GetUnionCases typeof<MenuItem>
  [ for c in cases -> FSharpValue.MakeUnion(c, [| |]) :?> MenuItem ]

type Model = {
    OpenMenus: Set<MenuItem>
}

type Msg =
  | OpenMenu of MenuItem
  | CloseMenu of MenuItem
  | ExpandAllMenus
