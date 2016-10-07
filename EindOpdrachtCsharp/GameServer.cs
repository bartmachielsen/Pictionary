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
        public const int amountToStart = 3;
        public const int maxAmount = 20;

        public List<GameServer> servers = new List<GameServer>();
        public List<GameSession> sessions = new List<GameSession>();
        

       

        public void addServer(GameServer server)
        {
            servers.Add(server);
            Console.WriteLine($"ADDING GAMESERVER {servers.Count} of {amountNeeded}");
            if (ready() && sessions.Count == 0 && allSessionsFinished()) 
            {
                
                Console.WriteLine($"CREATED SESSION {sessions.Count}");
                GameSession session = new GameSession();
                session.participants = servers;
                session.selectDrawer();
                sessions.Add(session);
                new Thread(()=>waitUntilReady(session)).Start();
            }
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

        public bool startReady()
        {
            return servers.Count >= amountToStart;
        }

        public bool ready()
        {
            return servers.Count >= amountNeeded;
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
        CORRECTANSWER
    }

}
