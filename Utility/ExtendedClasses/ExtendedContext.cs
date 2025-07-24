using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.ExtendedClasses
{
  public class ExtendedContext : SocketCommandContext
  {
    public string[]? Args;
    public ExtendedContext (DiscordSocketClient Client, SocketUserMessage Message, string[] Args) : base (Client, Message)
    {
      this.Args = Args;
    }
  }
}
