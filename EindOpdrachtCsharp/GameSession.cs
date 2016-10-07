using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtCsharp
{
    class GameSession
    {
        public List<GameServer> participants = new List<GameServer>();
        public string[] options;
        public string[] hints;
        public string answer;
        public bool finished = false;
        public string drawer;

        public GameSession()
        {
            options = new string[] {"Test1", "Test2" , "Test3" , "Real Answer"};
            hints = new string[] { "Hint1", "Hint2", "Hint3"};
            answer = "Real Answer";
            
            
        }

        public void listenToDrawer(object data)
        {
            
        }

        public void selectDrawer()
        {
            Random random = new Random();
            GameServer server = participants.ElementAt(random.Next(0,participants.Count));
            server.drawNotifier += pointDrawn;
            server.sendData(CommandsToSend.DRAWER);
            drawer = server.ToString();
            // TODO notify drawer with answer!
            // TODO block other persons ability to draw (and send)
            // TODO set the previous drawer on not drawable
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
                    participant.sendData(new SessionDetails(options, hints, drawer, answer));
                else
                    participant.sendData(new SessionDetails(options, hints, drawer));
                
            }
        }

        public void pointDrawn(DrawPoint draw)
        {
            foreach (var server in participants)
                if (!server.drawer)
                    server.sendData(draw);

        }
    }



}
