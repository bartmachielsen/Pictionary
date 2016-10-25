using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    class GameSession
    {
        public delegate void SessionListener(GameSession session);


        public SessionListener stateListener;

        public const int maximumGuesses = 3;

        public List<GameServer> participants = new List<GameServer>();
        public string[] alloptions;
        public string[] options = new string[100];
        public string[] hints;
        public Timer timer;
        public int id { get; set; }
        public Random random = new Random();
        public string answer;
        public bool finished = false;
        public string drawer;

        private SessionScore score;


        private static string[] randomUserNames =
        {
            "PirateHacker", "MasterDrawer", "DrawForLife", "RandomPlayer",
            "Hacker"
        };

        public GameSession()
        {
            alloptions = File.ReadAllLines("../../Resources/options.txt");
            for (int i = 0; i < options.Length; i++)
            {
                int index = random.Next(0, alloptions.Length);
                options[i] = alloptions[index];
            }

            score = new SessionScore();
        }


        public void uploadParticipants(List<GameServer> participants)
        {
            this.participants = participants;
            foreach (var participant in participants)
            {
                participant.scores.Add(new PlayerScore());

                if (participant.name == null)
                    participant.name = getRandomUserName();
                participant.latestScore().name = participant.name;
            }

        }

        private string getRandomUserName()
        {
            bool found = false;
            string manual = null;
            Random random = new Random();
            List<string> alreadyTaken = new List<string>();
            while (!found)
            {
                string guessed = randomUserNames[random.Next(0, randomUserNames.Length)];
                if (manual != null)
                    guessed = manual;
                bool taken = false;
                foreach (var participant in participants)
                {
                    taken = participant.name == guessed;
                    if (taken)
                        break;
                }
                if (!taken)
                    return guessed;
                else
                {
                    if(!alreadyTaken.Contains(guessed))
                        alreadyTaken.Add(guessed);

                    if (alreadyTaken.Count == randomUserNames.Length)
                        manual = randomUserNames[random.Next(0, randomUserNames.Length)] + random.Next(0, 100);
                }
            }
            return null;
        }

        public void selectDrawer()
        {
            Random random = new Random();
            GameServer server = participants.ElementAt(random.Next(0,participants.Count));
            server.notifyOnData += parseDataFromDrawer;
            server.sendData(CommandsToSend.DRAWER);
            server.drawer = true;
            drawer = server.name;
            Console.WriteLine("SELECTED DRAWER IS " + drawer + " " + server);
            foreach (var watcher in participants)
                if (watcher != server)
                    watcher.notifyOnData += parseDataFromWatchers;
                
            

        }

        public bool allReady()
        {
            foreach (var participant in participants)
                if (!participant.ready) return false;
            return true;
        }

        public void notifyAllParticipants()
        {
            SessionDetails details = new SessionDetails();
            details.participants = new List<string>();
            foreach (var participant in participants)
                details.participants.Add(participant.name);
            
            details.drawer = drawer;
            details.options = options;
            
            foreach (var participant in participants)
            {
                details.isDrawer = drawer == participant.name;
                details.name = participant.name;
                participant.sendData(details);

            }
        }

        public void parseDataFromWatchers(object obj, object sender)
        {
            Console.WriteLine(sender);
            GameServer gameSender = (GameServer) sender;
            if (obj is message)
            {
                message messag = (message)obj;
                switch ((CommandsToSend)messag.command)
                {
                    case CommandsToSend.GUESS:
                        if (gameSender.latestScore().wrongguesses.Count >= 3 || finished)
                        {
                            gameSender.sendData(CommandsToSend.BLOCKEDFROMGUESSING);
                            return;
                        }
                        if (answer != messag.data.ToString())
                        {
                            gameSender.sendMessage(CommandsToSend.WRONGANSWER,messag.data);
                            gameSender.latestScore().wrongguesses.Add(messag.data.ToString());
                            if (gameSender.latestScore().wrongguesses.Count >= maximumGuesses)
                            {
                                gameSender.sendData(CommandsToSend.BLOCKEDFROMGUESSING);
                            }
                        }
                        else
                        {
                            gameSender.sendMessage(CommandsToSend.CORRECTANSWER, messag.data);
                            finish(gameSender);
                        }
                        break;
                }
            }
        }

        public void finish(GameServer winner)
        {
            timer.Stop();

            winner.latestScore().answer = answer;
            winner.latestScore().timeScore = 1000 - ((int)score.totalTime.TotalSeconds*10);
            if (winner.latestScore().timeScore < 200)
                winner.latestScore().timeScore = 200;

            foreach (var participant in participants)
            {
                if (participant.drawer)
                {
                    participant.latestScore().timeScore = winner.latestScore().timeScore;
                }
                score.players.Add(participant.latestScore());
            }

            
            sendAllParticipants(score);

            finished = true;
            stateListener.Invoke(this);
        }

        public void parseDataFromDrawer(object obj,object sender)
        {
            if (obj is DrawPoint)
                pointDrawn((DrawPoint) obj);

            if (obj is CommandsToSend)
            {
                switch ((CommandsToSend) obj)
                {
                    case CommandsToSend.CLEARPANEL:
                        sendAllParticipants(obj);
                        break;
                }
            }
            if (obj is message)
            {
                message messag = (message) obj;
                switch ((CommandsToSend)messag.command)
                {
                        case CommandsToSend.ANSWER:
                        sendAllParticipants(CommandsToSend.STARTGUESSING);
                        answer = messag.data + "";
                        timer = new Timer(1000);
                        timer.Elapsed += updatetime;
                        timer.Enabled = true;
                        break;

                       
                }
            }
        }

        public void updatetime(object sender, EventArgs args)
        {
            if(score.totalTime == null)
                score.totalTime = new TimeSpan(0,0,0,0);
            score.totalTime = score.totalTime.Add(new TimeSpan(0, 0, 0, 1));
        }

        public void sendAllParticipants(object send)
        {
            foreach (var server in participants)
                server.sendData(send);
        }


        public void pointDrawn(DrawPoint draw)
        {
            foreach (var server in participants)
                if (!server.drawer)
                    server.sendData(draw);

        }
    }



}
