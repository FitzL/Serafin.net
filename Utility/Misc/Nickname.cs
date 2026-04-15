using Discord;
using Discord.WebSocket;
using MongoDB.Bson;
using Serafin.NET.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.Misc
{
  public static partial class Helper
  {
    public static string GetNickname(IGuildUser User)
    {
      var user = User as SocketGuildUser;

      if (user == null) return null;

      if (user.Nickname != "" || user.Nickname != null) return user.Nickname;
      return user.GlobalName;
    }
    public static string GetNickname(SocketUser User)
    {
      var user = User as SocketGuildUser;

      if (user == null) return null;

      if (user.Nickname != null) return user.Nickname;
      return user.GlobalName;
    }
  }
}
