using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    class GameServer : TCPConnector
    {
        public GameServer(TcpClient client) : base(client)
        {
        }
    }
}
