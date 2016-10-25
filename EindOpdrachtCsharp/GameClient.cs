using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    public class GameClient : TCPConnector
    {
        public delegate bool NameChecker(string newName);

        public string answer;


        public bool drawer;
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
                switch ((CommandsToSend) obj)
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

    [Serializable]
    public class SessionDetails
    {
        public string drawer;
        public bool isDrawer;
        public string name;
        public string[] options;
        public List<string> participants;

        public int sessionid;
    }

    [Serializable]
    public class DrawPoint
    {
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

        public double x { get; set; }
        public double y { get; set; }
        public double prevx { get; set; }
        public double prevy { get; set; }

        public int width { get; set; }

        public Color color { get; set; }
    }
}