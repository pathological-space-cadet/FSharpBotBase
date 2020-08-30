namespace FSharpEchobot.Bots

// open System
open System.Collections.Generic
open System.Threading
open System.Threading.Tasks
open FSharp.Control.Tasks.V2

open Microsoft.Bot.Builder
open Microsoft.Bot.Schema

type EchoBot () =
  inherit ActivityHandler ()

  override x.OnMembersAddedAsync (membersAdded: IList<ChannelAccount>, 
                                  ctx: ITurnContext<IConversationUpdateActivity>, 
                                  cancellationToken: CancellationToken) =
    task {
      let welcomeText = "Hello and Welcome"
      let mbs = Seq.filter (fun (m: ChannelAccount) -> m.Id.Equals(ctx.Activity.Recipient.Id)) membersAdded
      for mb in mbs do
        do! ctx.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken) :> Task
    } :> _

    override x.OnMessageActivityAsync (ctx: ITurnContext<IMessageActivity>, cancellationToken: CancellationToken) =
      let replyText = sprintf "Echo: %s" ctx.Activity.Text
      task {
        do! ctx.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken) :> Task
      } :> _