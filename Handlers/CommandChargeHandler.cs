using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Serafin.NET.Database;
using Serafin.NET.Database.Models;
using Serafin.NET.Utility.Misc;
using Serafin.NET.Utility.Preconditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Handlers
{
  public class CommandChargeHandler
  {
    public readonly MongoConnection? mongoConnection;
    public User? MongoUser;

    public CommandChargeHandler(CommandService CommandService)
    {
      mongoConnection = Global.MongoConnection;
      CommandService.CommandExecuted += Handle;
    }

    public async Task Handle( Optional<CommandInfo> Command, ICommandContext Context, IResult Result) 
    {
      if (!Command.IsSpecified) return;

      if (!Result.IsSuccess) {
        Console.WriteLine($"{Command.Value.Name} falló.\n" + Result);
        return; 
      }

      PriceAttribute? Price = Command.Value.Preconditions.OfType<PriceAttribute>().FirstOrDefault();

      if (Price == null || Price.Price < 1) return;

      int Cost = Price.Price;

      MongoUser = mongoConnection.GetUser(Context.Message.Author.Id);

      if (MongoUser == null) return;

      MongoUser.TransferCurrency(Global.Serafin, Cost);

      var embed = new EmbedBuilder()
      {
        Description = $"-{Cost}{Global.Serafin.serverCurrency}",
        Color = Helper.GetUserColor(await Context.Guild.GetCurrentUserAsync())?? null
      };

      await Context.Message.Channel.SendMessageAsync(embed: embed.Build() );
    }
  }
}
