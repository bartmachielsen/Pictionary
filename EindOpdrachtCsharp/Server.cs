using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    public class GameServer : TCPConnector
    {
        public delegate bool CheckName(string username);

        public CheckName checkName;

        public List<PlayerScore> scores = new List<PlayerScore>();

        public int serverID = 0;

        public bool alive = true;

        public GameServer(TcpClient client) : base(client)
        {
        }

        public bool ready { get; set; }
        public bool drawer { get; set; }

        public string name { get; set; }

        public bool staticName { get; set; }

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
                switch ((CommandsToSend) obj)
                {
                    case CommandsToSend.CONNECT:
                        ready = true;
                        break;
                }
            if (obj is Message)
            {
                var message = (Message) obj;
                if (message.command == CommandsToSend.NEWUSERNAME)
                {
                    var username = message.data + "";
                    if (username == "DELETE")
                    {
                        staticName = false;
                        return;
                    }

                    if ((checkName != null) && checkName.Invoke(username))
                    {
                        name = username;
                        staticName = true;
                        sendMessage(CommandsToSend.NEWUSERNAME, name);
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