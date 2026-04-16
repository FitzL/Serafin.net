using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Misc;
using Serafin.NET.Utility.Preconditions;
using Sprache;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks;


namespace Serafin.NET.Commands.Misc
{

  [Summary("Deten el server de Minecraft.")]
  public class StopServer : ModuleBase<ExtendedContext>
  {
    [Command("Stop")]
    [Alias("StopServer")]
    [Price(50)]
    [RequireRole(1401930182130008155)]
    
    public async Task Stop()
    {
      var Message = Context.Message as SocketUserMessage;
      if (Message == null) return;
      if (Message.Source == MessageSource.Webhook) return;

      var mem = Message.Author as IGuildUser;
      string displayName = mem.Nickname ?? mem.DisplayName ?? mem.GlobalName ?? mem.Username;

      var rcon = new RconClient();

      rcon.Connect("localhost", 25575, "password123");

      string response = rcon.SendCommand("say <" + displayName + "> ESTOY DETENIENDO EL SERVER PQ SOY UN HIJO DE PUTA");

      Thread.Sleep(5000);
       
      response = rcon.SendCommand("stop");

      return;
    }

  }
}
