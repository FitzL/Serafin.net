using Discord;
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
    private readonly MongoConnection mongoConnection = Global.MongoConnection;
    public UpdateHandler()
    {
      Global.Client.MessageReceived += UserUpdates;
    }

    public async Task UserUpdates(SocketMessage RawMessage)
    {
      var Message = RawMessage as SocketUserMessage;
      if (Message.Source == MessageSource.Webhook) return;

      var user = mongoConnection.GetUser(Message.Author.Id);
      if (user == null) {
        Console.WriteLine($"User ${Message.Author.Username}({Message.Author.Id}) Not Found or null");
        user = new User { _id = Message.Author.Id.ToString(), username = Message.Author.Username};
        mongoConnection.InsertUser(user);
      }

      Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {Message.Author.Username}");

      if (user.lastActivity == -1) 
      { 
        user.Kyu();
        user.Update();
        return;
      }

      user.UpdateLastActivity();
      user.UpdateXp();
      user.DoPay();

      user.Update(); 
      return;
    }
  }
}
