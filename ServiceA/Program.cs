/*
    SERVICE - A
*/

using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Client.Service;

public static class ServiceA
{
    static ISerivceA serviceA = new SerivceA();

    private static TcpListener server = new TcpListener(new IPEndPoint(IPAddress.Loopback, 4222));

    public static void Main()
    {
        server.Start();
        serviceA.Start(server);
    }
}
