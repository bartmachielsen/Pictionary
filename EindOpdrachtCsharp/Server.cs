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
        
        public bool ready { get; set; }
        public bool drawer { get; set; }

        public GameServer(TcpClient client) : base(client)
        {

        }

        public override void sendData(object data)
        {
            base.sendData(data);
            if (data == (object) CommandsToSend.DRAWER) drawer = true;

        }

        public override void parseReceivedObject(object obj)
        {
            base.parseReceivedObject(obj);
            
            if (obj is CommandsToSend)
            {
                switch ((CommandsToSend)obj)
                {
                        case CommandsToSend.CONNECT:
                        ready = true;
                        break;
                }
            }
        }

    }
    [Serializable]
    public struct Answer
    {
        public string answer { get; set; }

        public Answer(string answer)
        {
            this.answer = answer;
        }
    }
}
