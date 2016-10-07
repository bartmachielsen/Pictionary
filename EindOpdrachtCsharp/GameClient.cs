using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    public class GameClient : TCPConnector
    {
        
        

        public bool drawer = false;
        

        public GameClient(TcpClient client) : base(client)
        {
        }

        public override void parseReceivedObject(object obj)
        {
            base.parseReceivedObject(obj);
            if (obj is CommandsToSend)
            {
                switch ((CommandsToSend)obj)
                {
                        case CommandsToSend.DRAWER:
                        drawer = true;
                        break;

                        case CommandsToSend.NEW_SESSION:
                        drawer = false;
                        break;
                        
                }
            }
        }

    }
    [Serializable]
    public struct SessionDetails
    {
        public string[] options;
        public string[] hints;
        public string answer;
        public string drawer;

        public SessionDetails(string[] options, string[] hints, string drawer, string answer = null)
        {
            this.options = options;
            this.hints = hints;
            this.answer = answer;
            this.drawer = drawer;
        }
    }

    [Serializable]
    public class DrawPoint
    {
        public double x { get; set; }
        public double y { get; set; }
        public double prevx { get; set; }
        public double prevy { get; set; }

        public Color color { get; set; }

        public DrawPoint(double x, double y, double prevx, double prevy, Color color)
        {
            this.x = x;
            this.y = y;
            this.prevx = prevx;
            this.prevy = prevy;
            this.color = color;
        }
    }
}
