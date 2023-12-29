/*
    SERVICE - B
*/

using System.Net;
using System.Net.Sockets;
using Client.Service;

static class ServiceB
{
    static IClientService clientService = new ClientService();
    static IListenerService listenerService = new ListenerService();

    private static readonly int port = listenerService.GetAvalablePort();
    private static TcpListener server = new TcpListener(new IPEndPoint(IPAddress.Loopback, port));

    public static void Main()
    {
        var message = "( Get port for endpoint C ) : " + ((IPEndPoint)server.LocalEndpoint).Port;
        var i = clientService.RequiestMessage("127.0.0.1", 4222, message);
        Console.WriteLine(i);
    }
}