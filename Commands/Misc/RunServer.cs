using Discord.Commands;
using Serafin.NET.Utility;
using Serafin.NET.Utility.ExtendedClasses;
using Serafin.NET.Utility.Misc;
using Serafin.NET.Utility.Preconditions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.Commands.Misc
{
  [Summary("Corre remotamente un servidor de minecraft en la PC de Fitz.")]
  public class MinecraftServer : ModuleBase<ExtendedContext>
  {
    [Command("MCS")]
    [Alias("Server")]
    [Price(300)]
    public async Task RunServer()
    {
      var Port = 25565;

      string? ServerStartBat = Environment.GetEnvironmentVariable("MINECRAFT_SERVER_JAR");

      if (ServerStartBat == null) return;

      var process = new Process();
      process.StartInfo.FileName = ServerStartBat;
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(ServerStartBat);
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;

      process.Start();

      await ReplyAsync($"Servidor corriendo en la ip:\n`{await Helper.GetMyPublicIp()}:{Port}`\n-# teto dox te da la ip directamente");
      return;
    }
  }
}
