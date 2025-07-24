using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Serafin.NET.Database;
using Serafin.NET.Database.Models;
using Serafin.NET.Utility.Misc;
using Serafin.NET;
using Serafin.NET.Utility.ExtendedClasses;

namespace Serafin.Commands.Base
{
  [Summary("See your profile!")]
  public class Profile : ModuleBase<ExtendedContext>
  {
    private readonly MongoConnection? mongoConnection = Global.MongoConnection;

    [Command("Profile")]
    [Alias("Perfil", "P")]
    public async Task PingAsync()
    {
      var user = mongoConnection.GetUser(Context.Message.Author.Id.ToString());

      var Embed = new EmbedBuilder()
      {
        Title = $"{Helper.GetNickname(Context.Message.Author)}",
        ThumbnailUrl = Context.Message.Author.GetAvatarUrl(),
        Color = Helper.GetUserColor(Context.Message.Author),
      }
      .AddField("Dinero", user.currency + Global.BotCurrency, inline: false)
      .AddField("Ingreso", user.GetPay() + Global.BotCurrency, inline: true)
      .AddField("XP", user.xp, inline: true)
      .AddField("Nivel", user.lvl + " ⬆️", inline: true)
      .AddField("Cajas", user.cajas + Global.BotBox, inline: true);
      await ReplyAsync(embed: Embed.Build());
      return;
    }
  }
}
