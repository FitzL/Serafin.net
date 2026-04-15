using Discord;
using Discord.Commands;
using Serafin.NET;
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
    [Price(50)]

    public async Task RunServer()
    {
      var Port = 25565;
      var logChannel = Global.Client.GetChannel(1493728654331740200) as ITextChannel;

      string? ServerStartBat = Environment.GetEnvironmentVariable("MINECRAFT_SERVER_JAR");

      if (ServerStartBat == null) return;

      var process = new Process(); process.StartInfo.FileName = "cmd.exe";

      process.StartInfo.Arguments = $"/c \"{ServerStartBat}\"";
      process.StartInfo.WorkingDirectory = Path.GetDirectoryName(ServerStartBat);
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true; 
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.CreateNoWindow = true;

      process.OutputDataReceived += (s, e) =>
      {
        if (e.Data == null) return ;

        logChannel.SendMessageAsync(e.Data);
      };

      process.ErrorDataReceived += (s, e) =>
      {
        if (e.Data != null)
          Console.WriteLine("[ERR] " + e.Data);
      };

      process.Start();

      process.BeginOutputReadLine();
      process.BeginErrorReadLine();

      try 
      {
        await Helper.UpdateARecord();
      }
      catch(Exception)
      {
        
      }
      await ReplyAsync($"Servidor corriendo en:\n`kyu.panchessco.space`");
      return;
    }
  }
}
