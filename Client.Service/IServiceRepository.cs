using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Service
{
    public interface IListenerService
    {
        public int GetAvalablePort();
        public Task<string> ReadBytesAsync(NetworkStream stream);

        public void SendResponse(NetworkStream stream, string message);
    }

    public class ListenerService : IListenerService
    {
        public int GetAvalablePort()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
                var port = ((IPEndPoint)socket.LocalEndPoint).Port;
                socket.Close();

                return port;
            }
        }

        public async Task<string> ReadBytesAsync(NetworkStream stream)
        {
            var receivedData = new StringBuilder();

            Byte[] bytes = new Byte[256];

            while (true)
            {
                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    var data = Encoding.UTF8.GetString(bytes, 0, i);
                    Console.WriteLine($"Server Received: {data}");

                    if (data != null)
                    {
                        receivedData.Append(data);
                        break;
                    }
                }

                if (receivedData.Length > 0)
                {
                    return receivedData.ToString();
                }

                Thread.Sleep(2000);
            }
        }

        public void SendResponse(NetworkStream stream, string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);

            stream.Write(msg, 0, msg.Length);
            Console.WriteLine($"Server Sent: {message}");
        }
    }

}
