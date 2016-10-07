using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
            GameServer server = new GameServer(client);
            new Thread(() => server.checkInfinite()).Start();
            dataServer.addServer(server);

        }
    }
}
