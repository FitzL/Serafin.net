using Discord.WebSocket;
using Discord;
using Newtonsoft.Json;
using DotNetEnv;
using Serafin.Handlers;
using Discord.Commands;

namespace Serafin
{
  public class Program
  {
    public static async Task Main()
    {
      Env.Load();

      string? AuthToken = Environment.GetEnvironmentVariable("AUTH_TOKEN");
      if (AuthToken == null) throw new Exception("No AuthToken Found.");

      DiscordSocketConfig Settings = new DiscordSocketConfig()
      {
        MessageCacheSize = 200,
        GatewayIntents = GatewayIntents.All
      };

      DiscordSocketClient Client = new DiscordSocketClient(Settings);

      await Client.LoginAsync(TokenType.Bot, AuthToken);
      await Client.StartAsync();

      CommandHandler commandHandler = new CommandHandler(Client, new CommandService());
      commandHandler.LoadCommands();

      Client.Ready += () =>
      {
        Console.WriteLine("Teto is connected!");
        return Task.CompletedTask;
      };

      await Task.Delay(-1);
    }
  }
}