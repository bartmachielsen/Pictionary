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
        public delegate bool CheckName(string username);

        public CheckName checkName;

        public int serverID = 0;
        
        public bool ready { get; set; }
        public bool drawer { get; set; }

        public string name { get; set; }

        public bool staticName { get; set; }= false;

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
            
            
            if (obj is CommandsToSend)
            {
                switch ((CommandsToSend)obj)
                {
                        case CommandsToSend.CONNECT:
                        ready = true;
                        break;
                }
            }
            if (obj is Message)
            {
                Message message = (Message) obj;
                if (message.command == CommandsToSend.NEWUSERNAME)
                {
                    string username = message.data + "";
                    if (username == "DELETE")
                    { 
                        this.staticName = false;
                        return;
                    }

                    if (checkName != null && checkName.Invoke(username))
                    {
                        this.name = username;
                        this.staticName = true;
                       this.sendMessage(CommandsToSend.NEWUSERNAME,name);
                    }
                }
            }
            base.parseReceivedObject(obj);
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
