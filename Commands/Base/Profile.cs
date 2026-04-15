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
using Discord.WebSocket;

namespace Serafin.Commands.Base
{
  [Summary("See your profile!")]
  public class Profile : ModuleBase<ExtendedContext>
  {
    private readonly MongoConnection? mongoConnection = Global.MongoConnection;

    [Command("Profile")]
    [Alias("Perfil", "P")]
    public async Task ProfileAsync(string keyword = null)
    {
      var _u = await Helper.GetUser(keyword, Context);

      User user;

      if (_u == null) { 
        user = mongoConnection.GetUser(Context.Message.Author.Id);
        _u = Context.Message.Author as IGuildUser;
      }
      else user = mongoConnection.GetUser((_u).Id);



      if (false) return;

      var Embed = new EmbedBuilder()
      {
        Title = $"{Helper.GetNickname(_u) ?? _u.Username}",
        ThumbnailUrl = _u.GetAvatarUrl(),
        Color = Helper.GetUserColor(_u),
      }
      .AddField("Dinero", user.currency + Global.Serafin.serverCurrency, inline: false)
      .AddField("Ingreso", user.GetPay() + Global.Serafin.serverCurrency, inline: true)
      .AddField("XP", user.xp, inline: true)
      .AddField("Nivel", user.lvl + " ⬆️", inline: true)
      .AddField("Cajas", user.cajas + Global.BotBox, inline: true);

      if (_u == null)
      {
        Embed.Title = $"{Helper.GetNickname(Context.Message.Author) ?? Context.Message.Author.Username}";
        Embed.ThumbnailUrl = Context.Message.Author.GetAvatarUrl();
        Embed.Color = Helper.GetUserColor(Context.Message.Author as IGuildUser);
      }

      await ReplyAsync(embed: Embed.Build());
      return;
    }
  }
}