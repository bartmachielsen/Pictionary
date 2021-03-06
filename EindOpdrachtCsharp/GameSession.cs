﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    internal class GameSession
    {
        public delegate void SessionListener(GameSession session);

        public const int maximumGuesses = 3;
        public const int maxOptions = 100;
        public const int maximumHints = 3;
        public const int maximumSessionLength = 3;


        private static readonly string[] randomUserNames =
        {
            "Jaap", "Kees", "Olaf", "Jan", "Piet", "Karin", "Els", "Mariet", "Kim",
            "Bart", "Nard", "Sjaak", "Sjan", "Maarten", "Jamie", "Koen", "Anne", "Barbie",
            "Eva", "Ilse", "Achmed", "Muis"
        };

        public string answer;

        public Option answerOption;
        public GameServer drawer;

        public bool finished;

        private readonly List<Option> options = new List<Option>();

        public List<GameServer> participants = new List<GameServer>();
        public Random random = new Random();

        public bool renewed = false;

        public SessionScore score;


        public SessionListener stateListener;

        public Timer timer;

        public GameSession()
        {
            var alloptions = File.ReadAllLines("../../Resources/options.txt");
            var hints = File.ReadAllLines("../../Resources/hints.txt");


            for (var i = 0; i < alloptions.Length; i++)
            {
                var keyword = alloptions[i];
                string hintsFullLength = null;
                foreach (var hintslength in hints)
                    if (hintslength.Split('-').Length >= 0)
                        if (hintslength.Split('-')[0] == keyword)
                            hintsFullLength = hintslength;

                string[] hintsarray = null;
                if (hintsFullLength != null)
                {
                    var splitted = hintsFullLength.Split('-');
                    hintsarray = new string[splitted.Length - 1];
                    for (var j = 1; j < splitted.Length; j++)
                        hintsarray[j - 1] = splitted[j];
                }
                options.Add(new Option(keyword, hintsarray));
            }
            options.Sort((o1, o2) => random.Next(0, 2));
            options = options.FindAll(o1 => random.Next(0, 2) >= 1);
            var maxOptions2 = maxOptions;
            if (maxOptions2 > options.Count)
                maxOptions2 = options.Count;
            options = options.GetRange(0, maxOptions2);
            score = new SessionScore();
        }

        public int id { get; set; }


        public void uploadParticipants(List<GameServer> participants)
        {
            this.participants = participants;
            foreach (var participant in participants)
            {
                participant.errorNotifier += participantError;
                participant.scores.Add(new PlayerScore());
                participant.latestScore().drawer = participant.drawer;
                participant.notifyOnData += parseDataFromAll;
                if (!participant.staticName || (participant.name == null))
                    participant.name = getRandomUserName();
                participant.latestScore().name = participant.name;
            }
        }

        public void participantError(TCPConnector.ErrorLevel errorLevel, string message, object participant)
        {
            if (finished) return;
            if (participant is GameServer && (errorLevel >= TCPConnector.allowedErrorLevel))
            {
                var server = (GameServer) participant;
                server.alive = false;
                server.close();

                this.participants.RemoveAll(serverPart => server.serverID == serverPart.serverID);

                if (server.drawer || (participants.Count <= DataServer.amountNeeded))
                {
                    finish(null);
                }
                else
                {
                    Console.WriteLine("PARTICIPANT REMOVED NEW SIZE:" + this.participants.Count);
                    var participantslist = new List<string>();
                    foreach (var partici in this.participants)
                        participantslist.Add(partici.name);
                    sendAllParticipants(CommandsToSend.PARTICIPANTSUPDATE, participantslist);
                }
            }
        }

        private string getRandomUserName()
        {
            var found = false;
            string manual = null;
            var random = new Random();
            var alreadyTaken = new List<string>();
            while (!found)
            {
                var guessed = randomUserNames[random.Next(0, randomUserNames.Length)];
                if (manual != null)
                    guessed = manual;
                var taken = false;
                foreach (var participant in participants)
                {
                    taken = participant.name == guessed;
                    if (taken)
                        break;
                }
                if (!taken)
                    return guessed;
                if (!alreadyTaken.Contains(guessed))
                    alreadyTaken.Add(guessed);

                if (alreadyTaken.Count == randomUserNames.Length)
                    manual = randomUserNames[random.Next(0, randomUserNames.Length)] + random.Next(0, 100);
            }
            return null;
        }

        public void selectDrawer()
        {
            var random = new Random();
            var server = participants.ElementAt(random.Next(0, participants.Count));
            server.notifyOnData += parseDataFromDrawer;
            server.sendData(CommandsToSend.DRAWER);
            server.drawer = true;
            drawer = server;
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
            var details = new SessionDetails();
            details.participants = new List<string>();
            foreach (var participant in participants)
                details.participants.Add(participant.name);

            details.drawer = drawer.name;
            details.options = new string[options.Count];
            for (var i = 0; i < details.options.Length; i++)
                details.options[i] = options.ElementAt(i).option;
            foreach (var participant in participants)
            {
                details.isDrawer = drawer.serverID == participant.serverID;
                details.name = participant.name;
                participant.sendData(details);
            }
        }

        public void parseDataFromWatchers(object obj, object sender)
        {
            Console.WriteLine(sender);
            var gameSender = (GameServer) sender;
            if (obj is Message)
            {
                var messag = (Message) obj;
                switch (messag.command)
                {
                    case CommandsToSend.GUESS:
                        if ((gameSender.latestScore().wrongguesses.Length >= 3) || finished)
                        {
                            gameSender.sendData(CommandsToSend.BLOCKEDFROMGUESSING);
                            return;
                        }
                        if ((answerOption != null) && (answerOption.option != messag.data.ToString()))
                        {
                            gameSender.sendMessage(CommandsToSend.WRONGANSWER, messag.data);
                            var guesses = new List<string>();
                            guesses.AddRange(gameSender.latestScore().wrongguesses);
                            guesses.Add(messag.data.ToString());
                            gameSender.latestScore().wrongguesses = guesses.ToArray();

                            if (gameSender.latestScore().wrongguesses.Length >= maximumGuesses)
                            {
                                gameSender.sendData(CommandsToSend.BLOCKEDFROMGUESSING);

                                var notUsed = false;
                                foreach (var participant in participants)
                                    if (!participant.drawer &&
                                        (participant.latestScore().wrongguesses.Length >= maximumGuesses))
                                    {
                                        notUsed = true;
                                        break;
                                    }

                                if (!notUsed)
                                    finish(null);
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
            if (obj is CommandsToSend)
                switch ((CommandsToSend) obj)
                {
                    case CommandsToSend.REQUESTHINT:
                        // SEND HINT OR SEND BLOCK OR SEND NO
                        if ((answerOption == null) || (answerOption.hints == null) || (answerOption.hints.Length == 0) || answerOption.hints.Length <= gameSender.latestScore().hintGuessed)
                            gameSender.sendMessage(CommandsToSend.REQUESTHINT, "NO");
                        else if (gameSender.latestScore().hintGuessed >= maximumHints)
                            gameSender.sendMessage(CommandsToSend.REQUESTHINT, "BLOCK");
                        else
                        {
                            gameSender.sendMessage(CommandsToSend.REQUESTHINT,
                                answerOption.hints[gameSender.latestScore().hintGuessed]);
                            gameSender.latestScore().hintGuessed++;
                        }
                        break;
                }
        }

        public void finish(GameServer winner)
        {
            if (finished) return;
            if (timer != null)
                timer.Stop();
            if (winner != null)
            {
                winner.latestScore().answer = answer;
                winner.latestScore().winner = true;
                winner.latestScore().timeScore = 1000 - (int) score.totalTime.TotalSeconds*10;
                if (winner.latestScore().timeScore < 200)
                    winner.latestScore().timeScore = 200;

                score.players = new PlayerScore[participants.Count];
                var index = 0;
                foreach (var participant in participants)
                {
                    if (participant.drawer)
                        participant.latestScore().timeScore = winner.latestScore().timeScore;
                    score.players[index] = participant.latestScore();
                    index++;
                }
            }
            sendAllParticipants(score);
            finished = true;

            foreach (var participant in participants)
                participant.notifyOnData = null;


            stateListener.Invoke(this);
        }

        public void parseDataFromDrawer(object obj, object sender)
        {
            if (obj is DrawPoint)
                pointDrawn((DrawPoint) obj);

            if (obj is CommandsToSend)
                switch ((CommandsToSend) obj)
                {
                    case CommandsToSend.CLEARPANEL:
                        sendAllParticipants(obj);
                        break;
                }
            if (obj is Message)
            {
                var messag = (Message) obj;
                switch (messag.command)
                {
                    case CommandsToSend.ANSWER:
                        sendAllParticipants(CommandsToSend.STARTGUESSING);


                        foreach (var option in options)
                            if (option.option == messag.data.ToString())
                            {
                                answerOption = option;
                                break;
                            }


                        timer = new Timer(1000);
                        timer.Elapsed += updatetime;
                        timer.Enabled = true;
                        break;
                }
            }
        }

        public void parseDataFromAll(object obj, object sender)
        {
            if (obj is Message)
            {
                var messag = (Message) obj;
                switch (messag.command)
                {
                    case CommandsToSend.NEWUSERNAME:
                        Console.WriteLine("SENDING UPDATE");
                        var participants2 = new List<string>();
                        foreach (var partici in participants)
                            participants2.Add(partici.name);
                        sendAllParticipants(CommandsToSend.PARTICIPANTSUPDATE, participants2);
                        break;
                }
            }
        }

        public void updatetime(object sender, EventArgs args)
        {
            if (score.totalTime == null)
                score.totalTime = new TimeSpan(0, 0, 0, 0);
            score.totalTime = score.totalTime.Add(new TimeSpan(0, 0, 0, 1));
            if (score.totalTime.TotalMinutes > maximumSessionLength)
            {
                finish(null);
                timer.Stop();
            }
        }

        public void sendAllParticipants(object send)
        {
            foreach (var server in participants)
                server.sendData(send);
        }

        public void sendAllParticipants(CommandsToSend command, object send)
        {
            foreach (var server in participants)
                server.sendMessage(command, send);
        }

        public void pointDrawn(DrawPoint draw)
        {
            foreach (var server in participants)
                if (!server.drawer)
                    server.sendData(draw);
        }

        public class Option
        {
            public Option(string option, string[] hints)
            {
                this.option = option;
                this.hints = hints;
            }

            public string option { get; set; }
            public string[] hints { get; set; }
        }
    }
}