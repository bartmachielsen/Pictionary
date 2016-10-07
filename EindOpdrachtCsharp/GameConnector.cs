using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtCsharp
{
    class GameConnector: ServerConnector
    {
        private DataServer dataServer;
        public GameConnector(int port) : base(port)
        {
            dataServer = new DataServer();
            startChecking();
        }

        public override void addServer(TcpClient client)
        {
            Console.WriteLine("ADDED NEW SERVER !");
            dataServer.addServer(new GameServer(client));
        }
    }
}
