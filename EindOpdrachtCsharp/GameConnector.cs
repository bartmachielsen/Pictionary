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
        private List<DataServer> dataServers = new List<DataServer>();
        public GameConnector(int port) : base(port)
        {
            startChecking();
        }

        public override void addServer(TcpClient client)
        {

            

            GameServer server = new GameServer(client);
            new Thread(() => server.checkInfinite()).Start();
            DataServer dataServer = searchOpenServer();
            dataServers.Add(dataServer);

            dataServer.addServer(server);

            Console.WriteLine("ADDED NEW SERVER !");
        }

        public DataServer searchOpenServer()
        {
            foreach (var server in dataServers)
                if (!server.full())
                    return server;
                
            return new DataServer();
        }
    }
}
