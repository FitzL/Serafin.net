using Discord.WebSocket;
using Discord;
using Newtonsoft.Json;
using DotNetEnv;
using Discord.Commands;
using Serafin.NET.Database;
using System.Runtime.CompilerServices;
using Serafin.NET.Handlers;
using Serafin.NET.Utility;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Misc;

namespace Serafin.NET
{
  public static class Global
  {
    public static MongoConnection? MongoConnection;
    public static HttpClient HttpClient = new HttpClient();
    public static DiscordSocketClient? Client;
    public static string BotCurrency = " 🥖";
    public static string BotBox = " 🥡";
  }

  public class Program
  {
    public static async Task Main()
    {
      // environment setup

      Env.Load();
#if DEBUG
      string? AuthToken = Environment.GetEnvironmentVariable("DEBUG_AUTH_TOKEN");
#else
      string? AuthToken = Environment.GetEnvironmentVariable("RELEASE_AUTH_TOKEN");
#endif
      if (AuthToken == null) throw new Exception("No AuthToken Found.");

      DiscordSocketConfig Settings = new DiscordSocketConfig()
      {
        MessageCacheSize = 200,
        GatewayIntents = GatewayIntents.All
      };

      DiscordSocketClient Client = new DiscordSocketClient(Settings);

      // Login and command loading

      await Client.LoginAsync(TokenType.Bot, AuthToken);
      await Client.StartAsync();

      // Console ready at end of Socket setup

      Client.Ready += () =>
      {
#if DEBUG
        Console.WriteLine("Teto is connected!");
#else
        Console.WriteLine("Seraphine is connected!");
#endif
        return Task.CompletedTask;
      };

      // Set up database connection

      var MongoURI = Environment.GetEnvironmentVariable("MONGO_URI");
      if (MongoURI == null || MongoURI.Length == 0) throw new Exception("No Mongo URI");

      var Database = Environment.GetEnvironmentVariable("DATABASE");
      if (Database == null || Database.Length == 0) throw new Exception("Database Not Especified");

      var MongoConnection = new MongoConnection(MongoURI, Database);

      // Set up global variables and events.

      Global.MongoConnection = MongoConnection;
      Global.Client = Client;

      ExtendedCommandService commandService = new ExtendedCommandService();

      UpdateHandler updater = new UpdateHandler();
      CommandHandler commandHandler = new CommandHandler(commandService);

      new CommandChargeHandler(commandService);

      await commandHandler.LoadCommands();
      Console.WriteLine(await Helper.GetMyPublicIp());

#if DEBUG
      commandHandler.LogCommands();
#endif

      await Task.Delay(-1);
    }
  }
}