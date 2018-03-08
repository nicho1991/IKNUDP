using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IKNClientUDP
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string ip = args [0];
			string command = args [1];
			ClientData n = new ClientData (ip,command);
		}
	}

	/// <summary>
	/// Client data.
	/// </summary>
	public class ClientData{
		const int PORT = 9000;
		public ClientData(string ip,string command)
		{
			var client = new UdpClient();
			IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), PORT); // endpoint where server is listening
			client.Connect(ep);

			// send data
			string cmd = command;
			byte[] data = new byte[1024];
			data = Encoding.ASCII.GetBytes (cmd);
			client.Send(data,data.Length);


			// then receive data
			var receivedData = client.Receive(ref ep);

			Console.Write("receive data from " + ep.ToString());

			Console.Read();
		}
	}
}
