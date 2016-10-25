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
        public delegate bool NameChecker(string newName);


        
        

        public bool drawer = false;
        public string answer = null;
        public string name = "";

        public GameClient(TcpClient client) : base(client)
        {
        }

        public override void sendMessage(CommandsToSend command, object data)
        {
            base.sendMessage(command, data);
            if (command == CommandsToSend.ANSWER)
                answer = data + "";
            
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
    public class SessionDetails
    {
        public string[] options;

        public string drawer;
        public string name;

        public int sessionid;
        public List<string> participants;
        public bool isDrawer;
        

        public SessionDetails()
        {
            
        }

    }

    [Serializable]
    public class DrawPoint
    {
        public double x { get; set; }
        public double y { get; set; }
        public double prevx { get; set; }
        public double prevy { get; set; }
        
        public int width { get; set; }

        public Color color { get; set; }

        public DrawPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public DrawPoint(double x, double y, double prevx, double prevy, Color color, int width = 1)
        {
            this.x = x;
            this.width = width;
            this.y = y;
            this.prevx = prevx;
            this.prevy = prevy;
            this.color = color;
        }
    }
}
