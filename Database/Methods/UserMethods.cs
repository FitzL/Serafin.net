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
    private bool CheckXpTimer()
    {
      return this.lastActivity > this.nextXp;
    }
    public void UpdateXp()
    {
      if (!CheckXpTimer()) return;

      int Gain = (int)Math.Floor(Math.Pow(Rand.NextDouble(), Xp.Bias) * Xp.Max) + Xp.Min;

      this.nextXp = this.lastActivity + Xp.Delay + Rand.Next(Xp.DelayVariance);

      this.xp += Gain;

      UpdateLevel();
    }

    public void UpdateLevel()
    {
      this.lvl = Math.Max((int)((Math.Log(this.xp / 2) + 1) / Math.Log(2)) + 1, 0);

    }

    public bool CheckPayTime()
    {
      return this.nextPay > this.lastActivity;
    }

    public int GetPay()
    {
      return Pay.BasePay +
      (Math.Min(this.currency, Pay.MaxInterestBalance) / Pay.InterestRatio) +
      (this.lvl / Pay.LevelInterest);
    }

    public void DoPay()
    {
      if (!CheckPayTime()) return;
      this.currency += GetPay();

      this.nextPay = this.lastActivity + Pay.WorkingHours + this.Rand.Next(Pay.WorkingHoursVariance);
    }

    public void UpdateLastActivity()
    {
      this.lastActivity = DateTime.Now.Ticks;
    }

  }
}
