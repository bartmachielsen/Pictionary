using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtCsharp
{
   

    class DataServer
    {
        
        public List<GameServer> servers = new List<GameServer>();

        

        public void addServer(GameServer server)
        {
            servers.Add(server);
            server.drawNotifier += pointDrawn;
        }

        public void pointDrawn(DrawPoint draw)
        {
            foreach (var server in servers)
                if(!server.drawer)
                    server.sendData(draw);
            
        }
    }
}
