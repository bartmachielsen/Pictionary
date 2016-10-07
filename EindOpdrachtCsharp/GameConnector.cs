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
        private GameServer gameServer;
        public GameConnector(int port) : base(port)
        {
            gameServer = new GameServer();
            startChecking();
        }

        public override void addServer(TcpClient client)
        {
            //gameServer.AddServer(new GameServer(client));
        }
    }
}
