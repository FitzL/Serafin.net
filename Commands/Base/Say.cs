using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Preconditions;
using Serafin.NET.Utility.Misc;

namespace Serafin.Commands.Base
{
  [Summary("Repite lo que quieras!")]
  public class Say : ModuleBase<ExtendedContext>
  {
    [Command("Say")]
    [Price(1)]
    [AdminCommand]
    public async Task SayAsync([Remainder] string message)
    {
      Console.WriteLine(String.Join(" ", Context.Args));

      if (Context.Args.Length < 0)
      {
        await ReplyAsync("No se que quieres que diga");
        return;
      }

      await Context.Channel.SendMessageAsync(Helper.CleanString(String.Join(" ", Context.Args)));
      await Context.Message.DeleteAsync();
      return;
    }
  }
}
