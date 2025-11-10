using Discord;
using Discord.Commands;
using Serafin.NET.Database;
using Serafin.NET.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.Preconditions
{
  public class PriceAttribute : PreconditionAttribute
  {
    public readonly int Price;
    private readonly MongoConnection? MongoConnection;

    public PriceAttribute()
    {
      this.MongoConnection = Global.MongoConnection;
    }
    public PriceAttribute(int Price) : this()
    {
      this.Price = Price;
    }

    public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext Context, CommandInfo command, IServiceProvider services)
    {
      if (Price < 1) return PreconditionResult.FromSuccess();
      User? User = MongoConnection.GetUser(Context.User.Id);

      if (User.currency < Price)
      {
        await Context.Message.ReplyAsync($"<:raoralaugh:1343492065954103336>");
        await Context.Message.Channel.SendMessageAsync($"Pobre");
        return PreconditionResult.FromError("NO_FUNDS"); 
      }

      return PreconditionResult.FromSuccess();
    }
  }
}
