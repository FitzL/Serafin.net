using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Serafin.NET.Utility.Misc
{
  public static partial class Helper
  {
    public static async Task<string> GetMyPublicIp()
    {
      HttpClient HttpClient = Global.HttpClient;
      var response = await HttpClient.GetAsync(new Uri("http://checkip.dyndns.org/"));
      var Address = await response.Content.ReadAsStringAsync();

      int first = Address.IndexOf("Address: ") + 9;
      int last = Address.LastIndexOf("</body>");

      Address = Address.Substring(first, last - first);
      return Address;
    }
  }
}
