using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    class GameSession
    {
        public List<GameServer> participants = new List<GameServer>();
        public string[] alloptions;
        public string[] options = new string[100];
        public string[] hints;
        public Random random = new Random();
        public string answer;
        public bool finished = false;
        public string drawer;


        public static string[] randomUserNames =
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
        }


        public void uploadParticipants(List<GameServer> participants)
        {
            this.participants = participants;
            foreach (var participant in participants)
                if (participant.name == null)
                    participant.name = getRandomUserName();
                
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
            drawer = server.ToString();

            foreach (var watcher in participants)
            {
                if (watcher != server)
                {
                    watcher.notifyOnData += parseDataFromWatchers;
                }
            }

        }

        public bool allReady()
        {
            foreach (var participant in participants)
                if (!participant.ready) return false;
            return true;
        }

        public void notifyAllParticipants()
        {

            foreach (var participant in participants)
            {
                if(participant.drawer)
                    participant.sendData(new SessionDetails(options, hints, drawer,participant.name));
                else
                    participant.sendData(new SessionDetails(options, hints, drawer, participant.name));
                
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
                        if (answer == messag.data.ToString())
                        {
                            gameSender.sendMessage(CommandsToSend.WRONGANSWER,messag.data);
                        }
                        else
                        {
                            gameSender.sendMessage(CommandsToSend.CORRECTANSWER, messag.data);
                        }
                        break;
                }
            }
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
                        answer = messag.data + "";
                        Console.WriteLine("ANSWER SETTED AS " + answer);
                        break;

                       
                }
            }
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
