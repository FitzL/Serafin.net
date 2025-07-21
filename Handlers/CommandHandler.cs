using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serafin.Commands.Base;
using Discord.Commands;
using System.Reflection;

namespace Serafin.Handlers
{
  public class CommandHandler
  {
    private readonly DiscordSocketClient? Client;
    private readonly CommandService? Commands;
    public CommandHandler(DiscordSocketClient Client, CommandService Commands) {
      this.Client = Client;
      this.Commands = Commands;
    }

    public async Task LoadCommands() {

      Client.MessageReceived += Handle;

      await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: null);
    }

    public async Task Handle(SocketMessage RawMessage)
    {
      var Message = RawMessage as SocketUserMessage;
      if (Message == null) return;

      var Context = new SocketCommandContext(Client, Message);
      await Commands.ExecuteAsync(
          context: Context,
          argPos: 0,
          services: null
        );
    }
  }
}
