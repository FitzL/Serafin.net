using Discord.Commands;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Misc;
using Serafin.NET.Utility.Preconditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Serafin.Commands.Base
{
  [Summary("Doxea a Fitz!")]
  public class Dox : ModuleBase<ExtendedContext>
  {
    [Command("Dox")]
    [Alias("Ip")]
    [Price(5)]
    public async Task DoxAsync(params string[] args)
    {
      await ReplyAsync($"Ip de Fitz:\n`{await Helper.GetMyPublicIp()}`");
      return;
    }
  }
}
