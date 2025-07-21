using Discord.WebSocket;
using Discord;
using Newtonsoft.Json;

namespace Serafin
{
  public class Program
  {

    public static async Task Main()
    {
      var SecretFile = File.ReadAllText("D:\\Docs\\Panchessco\\Serafin.net\\Serafín\\secret.json");

      Dictionary<string, string> Secret = JsonConvert.DeserializeObject<Dictionary<string, string>>(SecretFile);

      DiscordSocketClient Client = new DiscordSocketClient();

      Console.WriteLine(Secret["AuthToken"]);

      await Client.LoginAsync(TokenType.Bot, Secret["AuthToken"]);
      await Client.StartAsync();

      Console.ReadLine();
    }
  }
}