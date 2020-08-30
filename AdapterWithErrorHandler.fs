namespace FSharpEchobot 

open System
open System.Threading.Tasks
open Microsoft.Bot.Builder
open Microsoft.Bot.Builder.Integration.AspNet.Core
open Microsoft.Bot.Builder.TraceExtensions
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging

open FSharp.Control.Tasks.V2

type AdapterWithErrorHandler (cfg: IConfiguration, logger: ILogger<BotFrameworkHttpAdapter>) as x =
  inherit BotFrameworkHttpAdapter (cfg, logger)
  
  do x.OnTurnError <- fun (ctx: ITurnContext) (e: exn) -> 
  (task {
    // Log any leaked exception from the application.
    logger.LogError(e, sprintf "[OnTurnError unhandled error : %A" e)

    // Send a message to the user.
    do! ctx.SendActivityAsync("The bot encountered an error or bug.") :> Task
    do! ctx.SendActivityAsync("To continue to run this bot, please fix the bot source code.") :> Task
    
    // Send a trace activity, which will be displayed in the Bot Framework Emulator.
    do! ctx.TraceActivityAsync("OnTurnError Trace", e.Message, "https://www.botframework.com/schemas/error", "TurnError") :> Task
  } :> _)

