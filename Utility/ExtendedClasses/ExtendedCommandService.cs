using Discord.Commands;
using Serafin.NET.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.ExtendedClasses
{
  public class ExtendedCommandService : CommandService
  {
    private readonly MongoConnection? mongoConnection;
    public async Task CheckBalance()
    {

      return;
    }
    public ExtendedCommandService() : base()
    {
      this.mongoConnection = Global.MongoConnection;
    }
  }
}
