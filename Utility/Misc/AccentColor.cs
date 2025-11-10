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
    //public static Color? GetUserColor(IGuildUser User) => GetUserColor((SocketUser) User) ;

    public static Color? GetUserColor(IGuildUser User)
    {
      Console.WriteLine(User);

      if (User.RoleIds == null) return null;

      List<IRole> colourRoles = new List<IRole>();

      foreach (var id in User.RoleIds) colourRoles.Add(User.Guild.GetRole(id));

      foreach (var role in colourRoles) Console.WriteLine(role);

      var color = colourRoles
          .OrderByDescending(r => r)
          .Where(r => r.Color.RawValue != 0)
          .FirstOrDefault()?.Color;

      if (color != null) return color;
      return null;
    }
  }
}
