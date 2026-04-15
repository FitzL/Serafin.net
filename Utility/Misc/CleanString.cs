using Discord;
using Discord.WebSocket;
using MongoDB.Bson;
using Serafin.NET.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.Misc
{
  public static partial class Helper
  {
    public static string CleanString(string input)
    {
      return Regex.Replace(
        input,
        @"@(everyone|here)\b",
        "`no`",
        RegexOptions.IgnoreCase
      );
    }
  }
}
