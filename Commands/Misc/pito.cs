using Discord;
using Discord.Commands;
using Serafin.NET;
using Serafin.NET.Database;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.Commands.Econ
{
  [Summary("Muestra tu pija al mundo")]
  public class Pito : ModuleBase<ExtendedContext>
  {
    private readonly MongoConnection? mongoConnection = Global.MongoConnection;

    [Command("Pito")]
    [Alias("pija", "pene", "pp")]
    public async Task PitoAsync(params string[] args)
    {
      await Context.Message.ReplyAsync("8" + new string('=', mongoConnection.GetUser(Context.Message.Author.Id).Rand.Next() % 32) + "D");
      return;
    }
  }
}
