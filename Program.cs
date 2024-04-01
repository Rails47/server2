using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartServer();
        }

        static void StartServer()
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.100.3");
            int port = 8888;
            TcpListener listener = new TcpListener(ipAddress, port);
            listener.Start();
            Console.WriteLine("Server started.");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
        }

        static void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received request: {data}");
            
            byte[] responseBuffer = Encoding.UTF8.GetBytes(data);
            stream.Write(responseBuffer, 0, responseBuffer.Length);
            client.Close();
        }
    }
}
