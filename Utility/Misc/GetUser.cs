using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Serafin.NET.Commands.Troll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.Misc
{
  public partial class Helper
  {
    public static async Task<IGuildUser?> GetUser(string KeyWord, ICommandContext Context)
    {
      if (string.IsNullOrWhiteSpace(KeyWord)) return null; 
      KeyWord = KeyWord.ToLower();

      var Users = await Context.Guild.GetUsersAsync();
      
      await foreach (var User in Users.ToAsyncEnumerable())
      {
        string nickname = User.Nickname ?? "";
        string username = User.Username ?? "";
        ulong userId = User.Id;
        string mention = "<@" + userId + ">";
        bool nick = nickname.ToLower().Contains(KeyWord);
        bool uname = username.ToLower().Contains(KeyWord);
        bool umention = mention.Contains(KeyWord);
        ulong uId;
        bool uid = ulong.TryParse(KeyWord, out uId) ? userId == uId : false;
        if (nick || uname || umention || uid) {
          try
          {
            return User;
          } 
          catch
          {
            continue;
          }
        }
      }

      return null;
    }
  }
}
