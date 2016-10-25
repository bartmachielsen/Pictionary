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
        private int currentID = 0;
        public GameConnector(int port) : base(port)
        {
            this.server = new DataServer();
            startChecking();
        }

        public override void addServer(TcpClient client)
        {

            

            GameServer server = new GameServer(client);
            server.serverID = currentID;
            currentID++;
            new Thread(() => server.checkInfinite()).Start();


            this.server.addServer(server);

        }

       
    }
}
