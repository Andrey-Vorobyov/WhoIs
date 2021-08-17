using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhoIs.IntegrationTests
{
    public static class TestTcpClient
    {
        // stream.Write seems blocks the server until stream is closed
        // WriteTimeout or other things I've tried didn't help
        // Decided to leave as I spent too much time here
        public static async Task<string> Connect(string message)
        {
            using var client = new TcpClient("127.0.0.1", 43);

            await using var stream = client.GetStream();

            var data = Encoding.ASCII.GetBytes(message);

            Console.WriteLine("Sent: {0}", message);

            //stream.WriteTimeout = 20;
            stream.Write(data, 0, data.Length);

            //Thread.Sleep(20);

            var buffer = new byte[1024];
            var response = new StringBuilder();

            while (stream.DataAvailable)
            {
                var read = stream.Read(buffer, 0, buffer.Length);
                response.Append(Encoding.ASCII.GetString(buffer, 0, read));
            }

            Console.WriteLine("Received: {0}", response);

            stream.Close();
            client.Close();

            return response.ToString();
        }
    }
}
