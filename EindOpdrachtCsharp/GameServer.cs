using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    internal class DataServer
    {
        public const string SessionScoreFile = "../../Resources/sessions.statistics";

        public static int amountNeeded = 3;
        public static int maxAmount = 3;

        public List<GameServer> servers = new List<GameServer>();

        public List<GameSession> sessions = new List<GameSession>();


        public List<GameServer> waiting = new List<GameServer>();


        public void addServer(GameServer server)
        {
            servers.Add(server);
            server.errorNotifier += deleteSafely;
            server.checkName += checkName;
            Console.WriteLine($"ADDING GAMESERVER {servers.Count} of {amountNeeded}");

            waiting.Add(server);

            server.sendData(CommandsToSend.WAITINGFORSESSION);

            if (waiting.Count >= amountNeeded)
                newSessionFromOldSession();
        }

        public bool checkName(string username)
        {
            foreach (var server in servers)
                if (server.name == username)
                    return false;
            return true;
        }

        private void deleteSafely(TCPConnector.ErrorLevel errorlevel, string errormessage, object sender)
        {
            if (sender is GameServer && ((int) errorlevel >= (int) TCPConnector.allowedErrorLevel))
            {
                Console.WriteLine(
                    $"SAFE ERROR WITH CONS HAS OCCURED LEVEL: SERVER \n errorlevel:{errorlevel} \n Message:{errormessage} \n server:{sender}");
                var server = (GameServer) sender;
                server.close();
                server.notifyOnData = null;

                waiting.RemoveAll(serverPart => server.serverID == serverPart.serverID);
                servers.RemoveAll(serverPart => server.serverID == serverPart.serverID);
            }
        }

        public void newSessionFromOldSession()
        {
            Console.WriteLine($"CREATED SESSION {sessions.Count} WITH {waiting.Count} WAITING");
            if (waiting.Count < amountNeeded)
                return;
            var session = new GameSession();
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

            writeSession(session.score);
            newSessionFromOldSession();
        }

        public void writeSession(SessionScore session)
        {
            var xs = new XmlSerializer(typeof(SessionScore[]));

            var scores = new SessionScore[0];
            if (File.Exists(SessionScoreFile))
                using (var stream = File.OpenRead(SessionScoreFile))
                {
                    scores = (SessionScore[]) xs.Deserialize(stream);
                }
            var scores2 = new List<SessionScore>(scores);
            scores2.Add(session);
            using (var stream = File.Create(SessionScoreFile))
            {
                xs.Serialize(stream, scores2.ToArray());
            }
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
        public DateTime Created;
        public PlayerScore[] players = new PlayerScore[5];
        public TimeSpan totalTime;

        public SessionScore()
        {
            Created = DateTime.Now;
        }

        public PlayerScore[] playerScore()
        {
            var players = new List<PlayerScore>(this.players);
            players.Sort(delegate(PlayerScore score, PlayerScore score1)
            {
                if (score == null) return 0;
                if (score1 == null) return 1;
                if (score.totalScore == score1.totalScore) return 0;
                if (score.totalScore > score1.totalScore) return -1;
                return 1;
            });

            return players.ToArray();
        }

        public void winner(out PlayerScore playerscore)
        {
            foreach (var player in players)
            {
                playerscore = player;
                if (player.winner)
                    return;
            }
            playerscore = null;
        }

        public override string ToString()
        {
            var text = "";
            foreach (var player in players)
                text += player + "\n";
            return text;
        }
    }

    [Serializable]
    public class PlayerScore
    {
        public string answer;
        public bool drawer = false;
        public int hintGuessed = 0;
        public string name;
        public int timeScore = 0;
        public bool winner = false;
        public string[] wrongguesses = new string[0];
        public int totalScore => (wrongguesses.Length + hintGuessed)*-200 + timeScore;

        public override string ToString()
        {
            return $"{name}:\t{totalScore}";
        }
    }
}