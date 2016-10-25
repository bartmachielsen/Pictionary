using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
   

    class DataServer
    {
        public static int amountNeeded = 3;
        public static int maxAmount = 3;

        public List<GameServer> servers = new List<GameServer>();


        public List<GameServer> waiting = new List<GameServer>();

        public List<GameSession> sessions = new List<GameSession>();
       

        public void addServer(GameServer server)
        {
            servers.Add(server);
            server.errorNotifier += deleteSafely;
            server.checkName += checkName;
            Console.WriteLine($"ADDING GAMESERVER {servers.Count} of {amountNeeded}");
            
            waiting.Add(server);

            server.sendData(CommandsToSend.WAITINGFORSESSION);
            
            if (waiting.Count >= amountNeeded)
            {
                newSessionFromOldSession();
            }
        }

        public bool checkName(string username)
        {
            foreach(GameServer server in servers)
                if (server.name == username)
                    return false;
            return true;
        }

        private void deleteSafely(TCPConnector.ErrorLevel errorlevel, string errormessage, object sender)
        {
            if (sender is GameServer && (int)errorlevel >= (int)TCPConnector.allowedErrorLevel)
            {
                Console.WriteLine($"SAFE ERROR WITH CONS HAS OCCURED LEVEL: SERVER \n errorlevel:{errorlevel} \n Message:{errormessage} \n server:{sender}");
                GameServer server = (GameServer)sender;
                server.close();
                this.waiting.RemoveAll((GameServer serverPart) => server.serverID == serverPart.serverID);
                this.servers.RemoveAll((GameServer serverPart) => server.serverID == serverPart.serverID);
            }
            else
            {
                Console.WriteLine($"SAFE ERROR WITH CONS HAS OCCURED LEVEL: SERVER \n errorlevel:{errorlevel} \n Message:{errormessage} \n server:{sender}");
            }
        }


        public void newSessionFromOldSession()
        {
            Console.WriteLine($"CREATED SESSION {sessions.Count} WITH {waiting.Count} WAITING");
            if (waiting.Count < amountNeeded)
                return;
            GameSession session = new GameSession();
            session.id = sessions.Count;
            if (waiting.Count > maxAmount)
            {
                session.uploadParticipants(waiting.GetRange(0, maxAmount));
                waiting.RemoveRange(0, maxAmount);
            }
            else
            {
                session.uploadParticipants(waiting);
                waiting = new List<GameServer>();
            }
            sessions.Add(session);
            
            new Thread(() => waitUntilReady(session)).Start();
            session.sendAllParticipants(CommandsToSend.NEW_SESSION);
            session.stateListener += clearSessionandNew;
        }

        public void clearSessionandNew(GameSession session)
        {
            waiting.AddRange(session.participants);
            foreach (var participant in session.participants)
                participant.sendData(CommandsToSend.WAITINGFORSESSION);
            newSessionFromOldSession();
        }

        public void waitUntilReady(GameSession session)
        {
            Console.WriteLine("WAITING TILL READY");
            while (!session.allReady())
            {
                
            }
            Console.WriteLine("ALL READY! START PLAYING MOTHAFOCKAS!");
            session.selectDrawer();
            session.notifyAllParticipants();
        }

        public bool allSessionsFinished()
        {
            foreach (var session in sessions)
                if (!session.finished) return false;
            return true;
        }

        
        public bool full()
        {
            return servers.Count >= maxAmount;
        }

    }

    [Serializable]
    public enum CommandsToSend
    {
        NEW_SESSION,
        DRAWER,
        CONNECT,
        CLEARPANEL,
        ANSWER,
        GUESS,
        WRONGANSWER,
        CORRECTANSWER,
        BLOCKEDFROMGUESSING,
        STARTGUESSING,
        PARTICIPANTSUPDATE,
        WAITINGFORSESSION,
        REQUESTHINT,
        NEWUSERNAME
       
    }

    [Serializable]
    public class SessionScore
    {
        public string winner;
        public TimeSpan totalTime;
        public List<PlayerScore> players  = new List<PlayerScore>();


        public PlayerScore[] playerScore()
        {
            players.Sort((delegate(PlayerScore score, PlayerScore score1)
            {
                if (score.totalScore == score1.totalScore) return 0;
                if(score.totalScore > score1.totalScore) return -1;
                return 1;
            }));

            return players.ToArray();
        }

        public override string ToString()
        {
            string text = "";
            foreach (var player in players)
            {
                text += player.ToString() + "\n";
            }
            return text;
        }
    }
    [Serializable]
    public class PlayerScore
    {
        public string name;
        public string answer;
        public int hintGuessed = 0;
        public List<string> wrongguesses = new List<string>();
        public int totalScore => ((wrongguesses.Count+hintGuessed)*-200) + timeScore;
        public int timeScore = 0;

        public override string ToString()
        {
            return $"{name}:\t{totalScore}";
        }
    }

}
