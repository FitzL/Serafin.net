using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serafin.Commands.Base;
using Discord.Commands;
using System.Reflection;
using Serafin.NET.Database;
using Serafin.NET;
using Serafin.NET.Utility.Misc;
using Serafin.NET.Utility.ExtendedClasses;
using System.Text.RegularExpressions;

namespace Serafin.NET.Handlers
{
  public class CommandHandler
  {
    private readonly DiscordSocketClient? Client;
    private readonly ExtendedCommandService Commands;
    private readonly MongoConnection? mongoConnection = Global.MongoConnection;
    private readonly string Prefix;
    public CommandHandler(ExtendedCommandService Commands)
    {
      this.Client = Global.Client;
      if (Client == null) throw new Exception("No Client Found");
      if (Commands == null) throw new Exception("No CommandService Found");
      this.Commands = Commands;
#if DEBUG
      var Prefix = Environment.GetEnvironmentVariable("DEBUG_PREFIX");
#else
      var Prefix = Environment.GetEnvironmentVariable("RELEASE_PREFIX");
#endif
      if (Prefix == null) throw new Exception("No Prefix Found");
      this.Prefix = Prefix;
    }

    public async Task LoadCommands() 
    {
      Client.MessageReceived += Handle;

      await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: null);
    }

    public void LogCommands()
    {
      foreach (var Command in this.Commands.Modules)
      {
        Console.WriteLine($"Loaded [{Command.Name}] (): {Command.Summary}");
      }
    }

    public async Task Handle(SocketMessage RawMessage)
    {
      var Message = RawMessage as SocketUserMessage;
      if (Message == null || !Message.Content.ToLower().StartsWith(Prefix) || Message.Author.IsBot) return;

      int argPos = Prefix.Length + 1;

      Console.WriteLine($"{Message.Author.GlobalName} @" + Message.CreatedAt);

      string[]? args = null;
      if (Message.Content.Substring(Prefix.Length + 1).Length > 0)
      {
        args = Regex.Split(Message.Content.Substring(Prefix.Length + 1), @"\s+").Skip(1).ToArray();
      }

      var Context = new ExtendedContext(Client, Message, args);

      await Commands.ExecuteAsync(
          context: Context,
          argPos: argPos,
          services: null
        );
    }
  }
}
