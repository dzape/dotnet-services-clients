using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Service
{
    public interface IExeptionService
    {
        public void HandleExeption(SocketException exeption);
    }

    public class ExeptionService : IExeptionService
    {



        public void HandleExeption(SocketException exeption)
        {
            if (exeption.ErrorCode == 61)
            {

            }
        }
    }

}
