using System.Net.Sockets;
using System.Threading;

namespace EindOpdrachtCsharp
{
    internal class GameConnector : ServerConnector
    {
        private int currentID;
        private readonly DataServer server;

        public GameConnector(int port) : base(port)
        {
            server = new DataServer();
            startChecking();
        }

        public override void addServer(TcpClient client)
        {
            var server = new GameServer(client);
            server.serverID = currentID;
            currentID++;
            new Thread(() => server.checkInfinite()).Start();


            this.server.addServer(server);
        }
    }
}