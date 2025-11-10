using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MongoDB.Driver.Linq;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Preconditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.Commands.Base
{
  [Summary("Callá Gil!")]
  public class Silencio : ModuleBase<ExtendedContext>
  {
    [Price(40)]
    [Command("Silencio")]
    [Testing]
    [Alias("SilencioGil", "Calla", "CallaGil")]
    public async Task SilencioGil(string message)
    {
      SocketUser? Mention = Context.Message.MentionedUsers.FirstOrDefault();

      if (Mention == null)
      {
        await Context.Message.ReplyAsync("Y que querés que haga exactamente?");
        return;
      }

      await Context.Guild.GetUser(Mention.Id).SetTimeOutAsync(
          TimeSpan.FromSeconds(30)
        );
      
      await Context.Channel.SendMessageAsync($"Silencio, gil. <@{Mention.Id}>");
      return;  
    }
  }
}
