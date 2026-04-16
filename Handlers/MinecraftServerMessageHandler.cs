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
  public class MinecraftServerMessageHandler
  {
    private readonly MongoConnection mongoConnection = Global.MongoConnection;
    public MinecraftServerMessageHandler()
    {
      Global.Client.MessageReceived += SendServerMessage;
    }

    public async Task SendServerMessage(SocketMessage RawMessage)
    {
      var Message = RawMessage as SocketUserMessage;
      if (Message == null) return;
      if (Message.Source == MessageSource.Webhook) return;

      var mem = Message.Author as IGuildUser;
      string displayName = mem.Nickname ?? mem.DisplayName ?? mem.GlobalName ?? mem.Username;

      if (Message.Channel.Id != 1493728654331740200) return;
      if (Message.Author.IsBot) return;

      //if (Global.Minecraft == null) return;
      //if (Global.Minecraft.HasExited) return;

      var rcon = new RconClient();

      rcon.Connect("localhost", 25575, "password123");

      string response = rcon.SendCommand("say <" + displayName + "> " + Message.Content);
    }
  }
}
