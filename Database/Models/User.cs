using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Serafin.NET.Database.Models
{
  public class User
  {
    public string _id {  get; set; }
    public string username { get; set; }
    public int xp {  get; set; }
    public int lvl { get; set; }
    public int currency { get; set; }
    public double lastActivity { get; set; }
    public double nextXp { get; set; }
    public double nextPay {  get; set; }
    public int cajas { get; set; }
    public bool payed { get; set; }
    [BsonIgnore]
    public Random Rand = new Random();
    private bool CheckXpTimer()
    {
      return this.lastActivity > this.nextXp;
    }
    public void UpdateXp()
    {
      if (!CheckXpTimer()) return;

      int Gain = (int) Math.Floor(Math.Pow(Rand.NextDouble(), Xp.Bias) * Xp.Max) + Xp.Min;

      this.nextXp = this.lastActivity + Xp.Delay + Rand.Next(Xp.DelayVariance);

      this.xp += Gain;
    }

    public void UpdateLevel()
    {
      this.lvl = Math.Max((int) ((Math.Log(this.xp / 2) + 1) / Math.Log(2)) + 1, 0);
      
    }

    public bool CheckPayTime()
    {
      return this.nextPay > this.lastActivity;
    }

    public int GetPay()
    {
      return Pay.BasePay +
      (Math.Min(this.currency, 5_000) / 250) +
      (this.lvl / 10);
    }

    public void DoPay() // TODO: implement pay method, should calculate income and add it to user
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
