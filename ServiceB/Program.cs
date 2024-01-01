/*
    SERVICE - B
*/

using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Client.Service;

static class ServiceB
{
    static IClientService clientService = new ClientService();
    static IListenerService listenerService = new ListenerService();

    private static readonly int port = listenerService.GetAvalablePort();
    private static TcpListener server = new TcpListener(new IPEndPoint(IPAddress.Loopback, port));

    private static string endpointC = "";

    public static void Main()
    {
        var rndNumber = new Random().Next(1, 6);

        var message = "( Get port for endpoint C ) : " + ((IPEndPoint)server.LocalEndpoint).Port;
        var request = clientService.RequiestMessage("127.0.0.1", 4222, message);

        endpointC = Regex.Match(request, @"\d+").Value;

        server.Start();

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            var response = listenerService.ReadBytesAsync(stream);

            var responseMessage = response.Result.Contains(rndNumber.ToString()) ? "OK Correct" : "OK Incorrect";
            listenerService.SendResponse(stream, responseMessage);

            if (responseMessage.Contains("Correct")) break;
        }
    }
}