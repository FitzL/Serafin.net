using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Serafin.NET.Database.Models;

public partial class User
{
  public string _id {  get; set; }
  public string username { get; set; }
  public int xp { get; set; } = 0;
  public int lvl { get; set; }
  public int currency { get; set; } = 10;
  public double lastActivity { get; set; } = 0;
  public double nextXp { get; set; } = 0;
  public double nextPay { get; set; } = 0;
  public int cajas { get; set; } = 0;
  public bool payed { get; set; } = false;
  public string? serverCurrency { get; set; } = null;
  public bool isBotTester { get; set; } = false;
  public bool isBotAdmin { get; set; } = false;
  public string lichess { get; set; } = null;
  public bool isLichessVerified { get; set; } = false;
  public string chesscom { get; set; } = null;
  public bool isChesscomVerified { get; set; } = false;
  public DateTime RegisteredDate { get; set; } = DateTime.Now;
  [BsonIgnore]
  public Random Rand = new Random();
}
