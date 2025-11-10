using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serafin.NET.Database.Models;

namespace Serafin.NET.Database.Models
{
  public partial class User
  {
    public async Task Kyu()
    {
      this.xp = 999;
      this.lvl = 99;
      this.currency = 9999;
      this.lastActivity = -1;
      this.nextXp = -1;
      this.cajas = 99;
    }
  }
}
