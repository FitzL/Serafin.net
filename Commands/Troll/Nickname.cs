using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Preconditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Commands.Troll
{
  [Summary("Cambia el nombre de un usuario.")]
  public class Nickname : ModuleBase<ExtendedContext>
  {
    [Price(25)]
    [Command("Name")]
    [Testing]
    [Alias("nick", "nickname")]
    public async Task Name(params string[] args)
    {
      SocketUser? Mention = Context.Message.MentionedUsers.FirstOrDefault();

      if (Mention == null)
      {
        await Context.Message.ReplyAsync("Y que querés que haga exactamente?");
        throw new Exception();
      }

      string? Name = String.Join(" ", Context.Args.Skip(1));

      if (Name == null)
      {
        await Context.Message.ReplyAsync("Y que querés que haga exactamente?");
        throw new Exception();
      }

      var target = Context.Guild.GetUser(Mention.Id);
      await target.ModifyAsync(u => u.Nickname = Name);
    }
  }
}
