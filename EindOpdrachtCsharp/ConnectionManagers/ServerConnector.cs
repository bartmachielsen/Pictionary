using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace EindOpdrachtCsharp
{
    public abstract class ServerConnector
    {
        private readonly TcpListener listener;

        public ServerConnector(int port)
        {
            this.port = port;
            listener = new TcpListener(LocalIPAddress(), port);
        }

        public int port { get; set; }

        public void startChecking()
        {
            listener.Start();
            while (true)
            {
                //Task<TcpClient> task = listener.AcceptTcpClientAsync();       DIEDED
                //task.ContinueWith(data => addServer(data.Result));

                //TcpClient client = listener.AcceptTcpClient();                UGLY FIX
                //addServer(client);


                var task = listener.AcceptTcpClientAsync(); // LESSS DIED?
                var client = task.Result;
                addServer(client);
            }
        }

        public abstract void addServer(TcpClient client);


        public static IPAddress LocalIPAddress()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return null;

            var host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}