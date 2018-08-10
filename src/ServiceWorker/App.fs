module App

open Fable.Core

[<Global>]
let self: Fable.Import.Browser.ServiceWorker = jsNative

self.addEventListener_install(fun e ->
  printfn "Install service worker"
)