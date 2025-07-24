using Discord.WebSocket;
using Serafin.NET.Database;
using Serafin.NET.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Handlers
{
  public class UpdateHandler
  {
    User? user;
    private readonly MongoConnection mongoConnection = Global.MongoConnection;
    public UpdateHandler()
    {
      Global.Client.MessageReceived += UserUpdates;
    }

    public async Task UserUpdates(SocketMessage RawMessage)
    {
      var Message = RawMessage as SocketUserMessage;
      if (Message == null) return;

      this.user = mongoConnection.GetUser(Message.Author.Id.ToString());
      if (user == null) {
        Console.WriteLine($"User ${Message.Author.Username}({Message.Author.Id}) Not Found or null");
        this.user = new User { _id = Message.Author.Id.ToString(), username = Message.Author.Username};
        mongoConnection.InsertUser(this.user);
      }

      user.UpdateLastActivity();
      user.UpdateXp();
      user.DoPay();
      user.UpdateLevel();

      mongoConnection.UpdateUser(user);
      return;
    }
  }
}
