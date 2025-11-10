using Discord.Commands;
using Serafin.NET.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serafin.NET.Database.Models;
using Discord;

namespace Serafin.NET.Utility.Preconditions
{
  public class AdminCommandAttribute : PreconditionAttribute
  {
    private bool isAdmind;
    private readonly MongoConnection mongoConnection;

    public AdminCommandAttribute()
    {
      mongoConnection = Global.MongoConnection;
      isAdmind = true;
    }

    public AdminCommandAttribute(bool isAdmin) : this()
    {
      this.isAdmind = isAdmin;
    }
    public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext Context, CommandInfo command, IServiceProvider services)
    {
      User? User = mongoConnection.GetUser(Context.User.Id);

      if (User == null) return PreconditionResult.FromError("User Doesn't Exist");
      if (!User.isBotAdmin)
      {
        await Context.Message.ReplyAsync("No, jodete\n<:ayweno:1167952675158110308>");
        return PreconditionResult.FromError("AdminCommand");
      }

      return PreconditionResult.FromSuccess();
    }
  }
}
