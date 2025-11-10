using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using Serafin.NET.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Database
{
  public partial class MongoConnection
  {
    string ConnectionString { get; }
    MongoClient Client;
    IMongoDatabase Database;
    IMongoCollection<User> Users;

    public MongoConnection(string ConnectionString, string Database) 
    {
      this.ConnectionString = ConnectionString;

      Client = new MongoClient(this.ConnectionString);

      this.Database = Client.GetDatabase(Database);

      this.Users = this.Database.GetCollection<User>("users");
    }

    public void Test() 
    {
      var EmptyFilter = Builders<User>.Filter.Empty;

      Console.WriteLine(Users.Find(EmptyFilter).First().ToJson());
    }
  }
}
