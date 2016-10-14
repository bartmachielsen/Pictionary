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
        private DataServer server;
        public GameConnector(int port) : base(port)
        {
            this.server = new DataServer();
            startChecking();
        }

        public override void addServer(TcpClient client)
        {

            

            GameServer server = new GameServer(client);

            new Thread(() => server.checkInfinite()).Start();


            this.server.addServer(server);

        }

       
    }
}
