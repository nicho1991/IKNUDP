using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;



namespace IKNServerUDP
{
	
	class MainClass
	{
		public static void Main (string[] args)
		{
			ServerData n = new ServerData();

		}
	}

	/// <summary>
	/// Does all the server stuff
	/// </summary>
	public class ServerData{
		const int PORT = 9000;
		string UPath =  @"/proc/loadavg";
		string LPath = @"/proc/uptime";
		public ServerData()
		{
			UdpClient udpServer = new UdpClient(PORT);
			FileStream fileU = new FileStream (UPath, FileMode.Open, FileAccess.Read);
			FileStream fileL = new FileStream (LPath, FileMode.Open, FileAccess.Read);
			byte[] data = new byte[1024];
			while (true)
			{
				//receive
				var remoteEP = new IPEndPoint(IPAddress.Any, PORT); 
				var cmd = udpServer.Receive(ref remoteEP); //receivng command
				var asciiCmd = AsciiConvert (cmd);
				Console.WriteLine("Received: " + AsciiConvert(asciiCmd));

				switch (asciiCmd) {
					case "U":
						break;

					case "u":
						break;

					case "L":
						break;

					case "l":
						break;

					default:
					string errorcmd = "Error! command not found";
					data = Encoding.ASCII.GetBytes (cmd);
					udpServer.Send(data,data.Length); // reply back

				}

			}
		}
		private string AsciiConvert(Byte[] ConvertThis){
			return Encoding.ASCII.GetString (ConvertThis);
		}
	}
}
