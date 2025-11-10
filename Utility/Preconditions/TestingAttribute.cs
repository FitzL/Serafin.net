using Discord;
using Discord.Commands;
using MongoDB.Driver;
using Serafin.NET.Database;
using Serafin.NET.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.Preconditions
{
  public class TestingAttribute : PreconditionAttribute
  {
    private bool isTesting;
    private readonly MongoConnection mongoConnection;
    public TestingAttribute()
    {
      mongoConnection = Global.MongoConnection;
      isTesting = true;
    }

    public TestingAttribute(bool isTesting) : this()
    {
      this.isTesting = isTesting;
    }
    public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext Context, CommandInfo command, IServiceProvider services)
    {
      User? User = mongoConnection.GetUser(Context.User.Id);

      if (User == null) return PreconditionResult.FromError("User Doesn't Exist");
      if (!User.isBotTester)
      {
        await Context.Message.ReplyAsync("No, jodete\n<:ayweno:1167952675158110308>");
        return PreconditionResult.FromError("WIP command");
      }

      return PreconditionResult.FromSuccess();
    }
  }
}
