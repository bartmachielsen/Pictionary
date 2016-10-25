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
        public int serverID = 0;
        
        public bool ready { get; set; }
        public bool drawer { get; set; }

        public string name { get; set; }

        

        public List<PlayerScore> scores = new List<PlayerScore>();

        
        public GameServer(TcpClient client) : base(client)
        {

        }

        public PlayerScore latestScore()
        {
            return scores.ElementAt(scores.Count - 1);
        }

        public override void sendData(object data)
        {
            base.sendData(data);
            

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
