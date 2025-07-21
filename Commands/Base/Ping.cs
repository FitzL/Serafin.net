using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Serafin.Commands.Base
{
  public class Ping : ModuleBase<SocketCommandContext>
  {
    [Command("Ping")]
    [Summary("Pong!")]
    public Task PingAsync()
    {
#if DEBUG
      Console.Write(Context.Message);
#endif
      return ReplyAsync("Pong!");
    }
  }
}
