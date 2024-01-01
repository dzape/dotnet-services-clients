using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Client.Service
{
    public interface ISerivceA
    {
        public void Start(TcpListener server);
    }

    public class SerivceA : ISerivceA
    {
        static string endpointB = "";
        static string endpointC = "";

        static IListenerService listenerService = new ListenerService();

        public void Start(TcpListener server)
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                var response = listenerService.ReadBytesAsync(stream);

                if (response.Result.Contains("endpoint C"))
                {
                    endpointB = Regex.Match(response.Result, @"\d+").Value;
                    var message = endpointC != "" ? "(OK) => ( Endpoint C ) " + endpointC.ToString() : "(404) Service C is not found";

                    listenerService.SendResponse(stream, message);
                }
                else if (response.Result.Contains("endpoint B"))
                {
                    endpointC = Regex.Match(response.Result, @"\d+").Value;
                    var message = endpointB != "" ? "(OK) => ( Endpoint B ) " + endpointB.ToString() : "(404) Service B is not found";

                    listenerService.SendResponse(stream, message);
                }
            }

        }
    }

}
