/*
    SERVICE - A
*/

using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Client.Service;

static class ServiceA
{
    static string endpointB = "";
    static string endpointC = "";

    static IListenerService listenerService = new ListenerService();
    private static TcpListener server = new TcpListener(new IPEndPoint(IPAddress.Loopback, 4222));

    public static void Main()
    {
        server.Start();

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            var response = listenerService.ReadBytesAsync(stream);
            Console.WriteLine(response.Result);

            if (response.Result.Contains("endpoint C"))
            {
                endpointB = Regex.Match(response.Result, @"\d+").Value;
                var message = endpointC != "" ? "( Endpoint C ) " + endpointC.ToString() : "(404) Service C is not found";

                listenerService.SendResponse(stream, message);
            }
            else if (response.Result.Contains("endpoint B"))
            {
                endpointC = Regex.Match(response.Result, @"\d+").Value;
                var message = endpointB != "" ? "( Endpoint B ) " + endpointB.ToString() : "(404) Service B is not found";

                listenerService.SendResponse(stream, message);
            }
        }
    }
}
