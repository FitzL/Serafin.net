using Discord;
using Discord.WebSocket;
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
    public static Color? GetUserColor(SocketUser User)
    {
      var user = User as SocketGuildUser;

      var color = user.Roles
          .Where(r => r.Color.RawValue != 0)
          .OrderByDescending(r => r.Position)
          .FirstOrDefault()?.Color;

      if (color != null) return color;
      return null;
    }
  }
}
