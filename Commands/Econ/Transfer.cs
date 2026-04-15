using Discord.Commands;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Serafin.Commands.Econ
{
  [Summary("")]
  public class Give : ModuleBase<ExtendedContext>
  {
    [Command("Give")]
    public async Task PingAsync(params string[] args)
    {
    }
  }
}
