namespace FSharpEchobot.Controllers

// open System
open System.Threading.Tasks
open FSharp.Control.Tasks.V2
open Microsoft.AspNetCore.Mvc
open Microsoft.Bot.Builder
open Microsoft.Bot.Builder.Integration.AspNet.Core

[<Route("api/messages")>]
[<ApiController>]
type BotController (adapter: IBotFrameworkHttpAdapter, bot: IBot) =
  inherit ControllerBase ()
  let bot = bot
  let adapter = adapter

  [<HttpPost>] [<HttpGet>]
  member x.PostAsync () : Task =
    task {
      do! adapter.ProcessAsync(x.Request, x.Response, bot)
    } :> _

   

