using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtCsharp
{
    abstract class ServerConnector
    {
        private TcpListener listener;
        public int port { get; set; }

        public ServerConnector(int port)
        {
            this.port = port;
            listener = new TcpListener(LocalIPAddress(),port);
        }

        public void startChecking()
        {
            listener.Start();
            while (true)
            {
                Task<TcpClient> task = listener.AcceptTcpClientAsync();
                task.ContinueWith(data => addServer(data.Result));

            }

        }
        public abstract void addServer(TcpClient client);


        public static IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
