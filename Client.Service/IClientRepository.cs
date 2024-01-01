using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Service
{
    public interface IClientService
    {
        public string RequiestMessage(string ip, int port, string message);
    }

    public class ClientService : IClientService
    {
        public string RequiestMessage(string ip, int port, string message)
        {
            var response = "";

            while (true)
            {
                try
                {
                    using (TcpClient client = new TcpClient(ip, port))
                    {
                        NetworkStream stream = client.GetStream();

                        response = SendMessage(stream, message);

                        Console.WriteLine($"Client Received: {response}");

                        if (response.Contains("OK")) break;

                        stream.Socket.Close();
                    }

                    Thread.Sleep(2000);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Server is not reachable. " + ex.Message);
                }
            }

            return response;
        }

        private string SendMessage(NetworkStream stream, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);

            data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string responseData = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine($"Client Sent: {responseData}");

            return responseData;
        }
    }
}
