using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;
using System.Net.Sockets;

public class RconClient
{
  private TcpClient client;
  private NetworkStream stream;
  private int requestId = 0;

  public void Connect(string host, int port, string password)
  {
    client = new TcpClient(host, port);
    stream = client.GetStream();

    // Login
    SendPacket(3, password);
    var response = ReadPacket();

    if (response.RequestId == -1)
      throw new Exception("RCON login failed");
  }

  public string SendCommand(string command)
  {
    SendPacket(2, command);
    var response = ReadPacket();
    return response.Payload;
  }

  private void SendPacket(int type, string payload)
  {
    requestId++;

    var payloadBytes = Encoding.ASCII.GetBytes(payload);
    int length = payloadBytes.Length + 10;

    using var ms = new MemoryStream();
    using var bw = new BinaryWriter(ms);

    bw.Write(length);
    bw.Write(requestId);
    bw.Write(type);
    bw.Write(payloadBytes);
    bw.Write((short)0); // null terminator

    var data = ms.ToArray();
    stream.Write(data, 0, data.Length);
  }

  public void Disconnect()
  {
    try
    {
      stream?.Close();
      client?.Close();
    }
    catch
    {
      // ignore errors on shutdown
    }
    finally
    {
      stream = null;
      client = null;
    }
  }

  private (int RequestId, string Payload) ReadPacket()
  {
    var br = new BinaryReader(stream);

    int length = br.ReadInt32();
    int reqId = br.ReadInt32();
    int type = br.ReadInt32();

    var payloadBytes = br.ReadBytes(length - 10);
    br.ReadInt16(); // skip null

    string payload = Encoding.ASCII.GetString(payloadBytes);
    return (reqId, payload);
  }
}