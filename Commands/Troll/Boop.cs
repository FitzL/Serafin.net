using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MongoDB.Driver.Linq;
using Serafin.NET.Database;
using Serafin.NET.Database.Models;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Misc;
using Serafin.NET.Utility.Preconditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.Commands.Base
{
  [Summary("Boop")]
  public class Boop : ModuleBase<ExtendedContext>
  {
    [Price(1)]
    [Command("Boop")]
    public async Task SilencioGil(string keyword = null)
    {
      var _u = await Helper.GetUser(keyword, Context);

      if (_u == null)
      {
        await Context.Message.ReplyAsync("Y que querés que haga exactamente?");
        throw new NotImplementedException();
      }

      await Context.Message.DeleteAsync();
      await Context.Channel.SendMessageAsync($"*boop* <@{_u.Id}>");
      return;
    }
  }
}
