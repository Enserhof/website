module App

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open Fable.PowerPack

[<Global>]
let self: Fable.Import.Browser.ServiceWorker = jsNative

let cacheName = "default-cache-v1"

self.addEventListener_install(fun event ->
  promise {
    let! cache = Browser.caches.``open`` cacheName
    return!
      !![
        "/index.html"
        "/client.js"
      ]
      |> cache.addAll
  }
  |> Promise.map (fun _ -> None)
  |> event.waitUntil
)

self.addEventListener_fetch(fun event ->
  let fetchAndCache (fetchRequest: Request) = promise {
    let! (response: Response) = !!Fetch.Fetch_types.GlobalFetch.fetch fetchRequest
    // Check if we received a valid response
    if !!response && response.status = 200. && response.``type`` = ResponseType.Basic
    then
      // IMPORTANT: Clone the response. A response is a stream
      // and because we want the browser to consume the response
      // as well as the cache consuming the response, we need
      // to clone it so we have two streams.
      let responseToCache = response.clone()

      let! cache = caches.``open`` cacheName
      do! cache.put(U2.Case1 event.request, responseToCache)

    return response
  }

  promise {
    let! response = caches.``match`` event.request
    // Cache hit - return response
    return!
      if !!response
      then Promise.lift response
      else
        // IMPORTANT: Clone the request. A request is a stream and
        // can only be consumed once. Since we are consuming this
        // once by cache and once by the browser for fetch, we need
        // to clone the response.
        fetchAndCache (event.request.clone())
  }
  |> U2.Case1
  |> event.respondWith
)
