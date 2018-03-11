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

			while (true)
			{

				UdpClient udpServer = new UdpClient(PORT);
				FileStream fileU = new FileStream (UPath, FileMode.Open, FileAccess.Read);
				FileStream fileL = new FileStream (LPath, FileMode.Open, FileAccess.Read);

				byte[] data = new byte[1024];
				Console.WriteLine("Started " +"Waiting for command ");

				//receive
				var remoteEP = new IPEndPoint(IPAddress.Any, PORT); 
				var cmdR = udpServer.Receive(ref remoteEP); //receivng command
				var asciiCmd = AsciiConvert (cmdR);
				//string System.Text.Encoding.Default.GetString();
				Console.WriteLine("Received: " + asciiCmd);

				switch (asciiCmd) {
					case "U":
					Console.WriteLine ("recieved U ");
					int Uinfo = fileU.Read (data, 0, data.Length);
					udpServer.Connect (remoteEP);
					udpServer.Send (data, data.Length);
					string s = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
					Console.WriteLine ("replied: " + s);
					udpServer.Close();
						break;

					case "u":
					Console.WriteLine ("recieved u ");
					int uinfo = fileU.Read (data, 0, data.Length);
					udpServer.Connect (remoteEP);
					udpServer.Send (data, data.Length);
					string s1 = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
					Console.WriteLine ("replied: " + s1);
					udpServer.Close();
						break;

					case "L":
					Console.WriteLine ("recieved L ");
					int Linfo = fileL.Read (data, 0, data.Length);
					udpServer.Connect (remoteEP);
					udpServer.Send (data, data.Length);
					string s2 = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
					Console.WriteLine ("replied: " + s2);
					udpServer.Close();
						break;

				case "l":
					Console.WriteLine ("recieved l ");
					int linfo = fileL.Read (data, 0, data.Length);
					udpServer.Connect (remoteEP);
					udpServer.Send (data, data.Length);
					string s3 = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
					Console.WriteLine ("replied: " + s3);
					udpServer.Close();
						break;

					default:
					string errorcmd = "Error! command not found";
					data = Encoding.ASCII.GetBytes (errorcmd);

					break;

				}

			}
		}
		private string AsciiConvert(Byte[] ConvertThis){
			return Encoding.ASCII.GetString (ConvertThis);
		}
	}
}
