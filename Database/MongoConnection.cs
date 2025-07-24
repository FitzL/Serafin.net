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
  public class MongoConnection
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

    public User? GetUser(string _id)
    {
#if !DEBUG
      Console.WriteLine($"Looking for user with id {_id} in MongoDB");
#endif
      var Filter = Builders<User>.Filter.Eq(u => u._id, _id);
      var user = Users.Find(Filter).FirstOrDefault();
      if (user == null) return null;
#if !DEBUG
      Console.WriteLine($"Found user {user.username} in MongoDB");
#endif
      return user;
    }

    public bool UserExists(User user)
    {
      var Filter = Builders<User>.Filter.Eq(u => u._id, user._id);
      var target = Users.Find(Filter).FirstOrDefault();
      return target != null;
    }

    public bool InsertUser(User user)
    {
      if (UserExists(user)) return false;
      Users.InsertOne(user);
#if DEBUG
      Console.WriteLine($"Added user {user.username.ToJson()} to MongoDB");
#endif
      return true;
    }

    public bool UpdateUser(User user)
    {
#if !DEBUG
      Console.WriteLine($"Updating user {user.username.ToJson()} in MongoDB");
#endif
      if (!UserExists(user)) throw new Exception("User doesn't exist");

      var result = Users.ReplaceOneAsync(
        Builders<User>.Filter.Eq(u => u._id, user._id),
        user
        );
#if !DEBUG
      Console.WriteLine($"Updated user {user.username} in MongoDB");
#endif
      return true;
    }

    public void Test() 
    {
      var EmptyFilter = Builders<User>.Filter.Empty;

      Console.WriteLine(Users.Find(EmptyFilter).First().ToJson());
    }
  }
}
