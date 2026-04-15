using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MongoDB.Bson;
using Serafin.NET;
using Serafin.NET.Database;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Misc;
using Serafin.NET.Utility.Preconditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;



namespace Serafin.Commands.Econ
{
  [Summary("Preguntale a teto")]

  public class Ask : ModuleBase<ExtendedContext>
  {
    private readonly MongoConnection? mongoConnection = Global.MongoConnection;
    private readonly DiscordSocketClient? DiscordClient = Global.Client;

    [Command("Ask")]
    [Price(10)]
    public async Task AskAsync(params string[] args)
    {
      using (Context.Channel.EnterTypingState())
      {
        HttpClient HttpClient = new HttpClient();
        string DeepSeekToken = Environment.GetEnvironmentVariable("DEEPSEEK_KEY");
        string DeepSeekURL = "https://api.deepseek.com/chat/completions";

        double tokenCost = 0.00000028;

        HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {DeepSeekToken}");

// -----------------------------------------------------------------------------------------------------

        // Assuming you already have the channel
        var channel = Context.Message.Channel as IMessageChannel;

        // Fetch last 50 messages
        var history = (await channel.GetMessagesAsync(50).FlattenAsync())
            .Where(m => m.Content.Length > 0)
            .OrderBy(m => m.Timestamp) // oldest → newest
            .ToList();

        var parsedMessages = new List<object>();

        // System
        parsedMessages.Add(new
        {
          role = "system",
          content =
                "You are Kasane Teto, you like bread. " +
                "Try to sneak jokes about bread when appropriate. " +
                "This is a group chat. " +
                "User messages are prefixed with usernames. " +
                "Always prioritize responding to the most recent user message." +
                "Here is a list of emojis (emojis are formatted in this way <:name:id>) with a brief context clue." +
                "<:dogexd:1126997588135125094> <:mikuxd:1126997691994488832> <:raoralaugh:1343492065954103336>, these are laughs" +
                "<:angrydog:1127627394283491358> <:kyumms:1312249649846816768> <:kyu:1201927709404713062>, these are general angry emojis" +
                "<:kyuhewwo:1446327652742070327> <:ergatou:1361716545612550322> <:iqdogo:1128114478316265593> <:peek:1306437352192872559> <:taweno:1126997352872431636> <:zzzz:1351003550398025789>, use these ones for pretty much anything." +
                "<:yuicry:1343632199609487360> <:yamero:1130304663397339207> <:kannakms:1167952749141442560> <:dogo:1127632208589492355>, these are general sad emojis." +
                "🍞 🥖 🫓 🥪, bread" +
                "<:agonized:1492913438626807929> <:chopper:1492914288376021225> <:blob:1492912970676834434> " +
                "<:disbelief:1492911346398920984> <:devil:1492910416181854278> <:crying:1492912002182418442> " +
                "<:coffee_gift:1492913499771502815> <:clown:1492909811526799543> <:chopper:1492914288376021225>" +
                "<:cat_face:1492914240720474323> <:blushing:1492912051813617776> <:disgusting:1492910219435704450> " +
                "<:dizzy:1492914330633765047> <:flattered:1492914549999931432> <:flower_gift:1492911476074348586>" +
                "<:flustered:1492911763866390703> <:fuming:1492913972813238312> <:embarrassed:1492912848941092864>" +
                "<:gasp:1492913926483087511> <:hiding:1492913830987169873> <:disbelief:1492911346398920984>, these have self explanatory names"
        });

        // History
        foreach (var message in history)
        {
          if (message.Id == Context.Message.Id)
            continue;

          bool isTeto = message.Author.Id == DiscordClient.CurrentUser.Id;
          bool isLastLine = message.Content.LastIndexOf('\n') > 0;

          parsedMessages.Add(
            new {
              role = (isTeto ? "assistant" : "user"),
              content = message.Content.StartsWith("teto ask", StringComparison.OrdinalIgnoreCase) 
                ? message.Content.Substring(8) 
                : (isTeto & isLastLine) 
                  ? message.Content[..message.Content.LastIndexOf('\n')] 
                  : message.Author.Username + ":" + message.Content
            });
        }

        // Latest message LAST
        parsedMessages.Add(new
        {
          role = "user",
          content = $"LATEST MESSAGE:\n{Context.Message.Author.Username}: {string.Join(" ", args)}"
        });

        // -----------------------------------------------------------------------------------------------------

        var bodyObj = new
        {
          model = "deepseek-chat",
          messages = parsedMessages
        };
        Console.Write(parsedMessages);

        var json = bodyObj.ToJson();
        var jsonBody = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync(DeepSeekURL, jsonBody);
        Console.WriteLine(response);
        var content = await response.Content.ReadAsStringAsync();
        var jsonContent = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(content);

        Console.WriteLine(content);

        string? Output = jsonContent["choices"][0]
          .GetProperty("message")
          .GetProperty("content")
          .GetString();

        double Cost = tokenCost * jsonContent["usage"]
          .GetProperty("total_tokens")
          .GetInt16();

        var chunkedOutput = Helper.SplitAtSpace(Helper.CleanString(Output) +"\n-# You took " + Cost.ToString("0.####################") + "$ from Fitz", 2000);

        try
        {
          bool isFirstMessage = true;
          foreach (var chunk in chunkedOutput)
          {
            if (isFirstMessage)
            {
              await Context.Message.ReplyAsync(chunk);
              isFirstMessage = false;
            }
            else Context.Message.Channel.SendMessageAsync(chunk);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          Context.Message.AddReactionAsync(Emote.Parse("<:yuicry:1343632199609487360>"));
        }

        return;
      }
    }
  }
}
