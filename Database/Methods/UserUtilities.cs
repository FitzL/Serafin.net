using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Database.Models
{
  public partial class User
  {
    public void Update()
    {
      Global.MongoConnection.UpdateUser(this);
    }

    public void SetCurrency(int amount)
    {
      this.currency = amount;
      this.Update();
    }

    public bool TransferCurrency(User Target, int amount)
    {
      if (amount > this.currency) return false;

      this.currency -= amount;
      Target.currency += amount;

      this.Update();
      Target.Update();

      return true;
    }

    public void AddBox(int amount)
    {
      this.cajas += amount;
      this.Update();
    }
  }
}
