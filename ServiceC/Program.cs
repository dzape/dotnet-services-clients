/*
    SERVICE - C
*/

using Client.Service;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

static class ServiceC
{
    static IClientService clientService = new ClientService();
    static IListenerService listenerService = new ListenerService();

    private static readonly int port = listenerService.GetAvalablePort();
    private static TcpListener server = new TcpListener(new IPEndPoint(IPAddress.Loopback, port));

    public static void Main()
    {
        var message = "( Get port for endpoint B ) : " + ((IPEndPoint)server.LocalEndpoint).Port;
        var portB = clientService.RequiestMessage("127.0.0.1", 4222, message);

        portB = Regex.Match(portB, @"\d+").Value;

        while (true)
        {
            var guess = new Random().Next(1, 6);
            var response = clientService.RequiestMessage("127.0.0.1", Int32.Parse(portB), guess.ToString());

            if (response.Contains("Correct"))
            {
                Console.WriteLine("( Client C guest the correct number !!! )");
                break;
            }
        }
    }
}
