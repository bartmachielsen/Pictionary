using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    public class GameClient : TCPConnector
    {
        public GameServer.PointDrawn drawNotifier;
        public GameClient(TcpClient client) : base(client)
        {
        }

        public override void parseReceivedObject(object obj)
        {
            Console.WriteLine("CLIENT HAS RECEIVED " + obj);
            if (obj is DrawPoint) drawNotifier.Invoke((DrawPoint)obj);
        }
    }



    [Serializable]
    public class DrawPoint
    {
        public double x { get; set; }
        public double y { get; set; }
        public double prevx { get; set; }
        public double prevy { get; set; }

        public DrawPoint(double x, double y, double prevx, double prevy)
        {
            this.x = x;
            this.y = y;
            this.prevx = prevx;
            this.prevy = prevy;
        }
    }
}
