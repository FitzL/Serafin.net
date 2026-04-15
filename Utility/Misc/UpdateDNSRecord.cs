using DotNetEnv;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.Misc
{
  public static partial class Helper
  {
    public static async Task<bool> UpdateARecord(string recordId = "4b9ef382d7e7363f47e1b4129c668e68")
    {
      HttpClient HttpClient = Global.HttpClient;

      string DNSToken = Environment.GetEnvironmentVariable("PANCHESSCO_SPACE_TOKEN");
      string ZoneID = Environment.GetEnvironmentVariable("PANCHESSCO_SPACE_ZONE_ID");
      
      string url = $"https://api.cloudflare.com/client/v4/zones/{ZoneID}/dns_records/{recordId}";

      HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {DNSToken}");

      var bodyObj = new
      {
        name = "mc-host.panchessco.space",
        ttl = 1,
        type = "A",
        comment = "Minecraft Server Record",
        content = await GetMyPublicIp(),
        proxied = true
      };

      var json = bodyObj.ToJson();
      var jsonBody = new StringContent(json, Encoding.UTF8, "application/json");

      //Console.WriteLine(url + "\n" + json);

      //return true;

      var response = await HttpClient.PatchAsync(url, jsonBody);
      var content = await response.Content.ReadAsStringAsync();

      Console.WriteLine(content);

      return false;
    }
  }
}
