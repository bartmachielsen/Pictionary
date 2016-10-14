using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EindOpdrachtCsharp
{
   

    class DataServer
    {
        public const int amountNeeded = 3;
        public const int maxAmount = 3;

        public List<GameServer> servers = new List<GameServer>();


        public List<GameServer> waiting = new List<GameServer>();

        public List<GameSession> sessions = new List<GameSession>();
       

        public void addServer(GameServer server)
        {
            servers.Add(server);
            Console.WriteLine($"ADDING GAMESERVER {servers.Count} of {amountNeeded}");
            
            waiting.Add(server);

            
            if (waiting.Count >= amountNeeded)
            {
                newSessionFromOldSession();
            }
        }

        public void newSessionFromOldSession()
        {
            Console.WriteLine($"CREATED SESSION {sessions.Count}");

            GameSession session = new GameSession();
            session.id = sessions.Count;
            if (waiting.Count > maxAmount)
            {
                List<GameServer> added = new List<GameServer>();
                added.AddRange(waiting.GetRange(0,maxAmount));
                added.RemoveRange(0,maxAmount);
                session.uploadParticipants(added);
            }
            else
            {
                session.uploadParticipants(waiting);
                waiting = new List<GameServer>();
            }
            sessions.Add(session);
            new Thread(() => waitUntilReady(session)).Start();
            session.sendAllParticipants(CommandsToSend.NEW_SESSION);
            session.selectDrawer();
            session.stateListener += clearSessionandNew;
        }

        public void clearSessionandNew(GameSession session)
        {
            waiting.AddRange(session.participants);
            newSessionFromOldSession();
        }

        public void waitUntilReady(GameSession session)
        {
            Console.WriteLine("WAITING TILL READY");
            while (!session.allReady())
            {
                
            }
            Console.WriteLine("ALL READY! START PLAYING MOTHAFOCKAS!");
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
        STARTGUESSING
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
