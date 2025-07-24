using Discord.Commands;
using Serafin.NET.Utility.ExtendedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Serafin.Commands.Base
{
  [Summary("Pong!")]
  public class Ping : ModuleBase<ExtendedContext>
  {
    [Command("Ping")]
    public async Task PingAsync()
    {
      await ReplyAsync("Pong!");
      return;
    }
  }
}
