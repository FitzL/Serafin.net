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
    public static IEnumerable<string> SplitAtSpace(string text, int maxLength = 2000)
    {
      int start = 0;

      while (start < text.Length)
      {
        int length = Math.Min(maxLength, text.Length - start);

        // If we're at the end, just take the rest
        if (start + length >= text.Length)
        {
          yield return text.Substring(start);
          yield break;
        }

        // Find last space before the limit
        int lastSpace = text.LastIndexOf(' ', start + length, length);

        if (lastSpace <= start)
        {
          // No space found → hard split
          yield return text.Substring(start, length);
          start += length;
        }
        else
        {
          // Split at space
          int chunkLength = lastSpace - start;
          yield return text.Substring(start, chunkLength);
          start = lastSpace + 1; // skip space
        }
      }
    }
  }
}
