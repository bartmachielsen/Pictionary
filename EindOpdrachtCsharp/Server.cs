using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    public class GameServer : TCPConnector
    {
        public delegate void PointDrawn(DrawPoint drawpoint);
        public PointDrawn drawNotifier;
        public bool drawer = false;

        public GameServer(TcpClient client) : base(client)
        {

        }

        public override void parseReceivedObject(object obj)
        {
            if (obj is DrawPoint)
            {
                drawer = true;
                drawNotifier.Invoke((DrawPoint) obj);
            }
        }
        
    }
}
