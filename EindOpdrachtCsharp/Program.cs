using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    class Program
    {
        static void Main(string[] args)
        {

            new Thread(()=> new GameConnector(180)).Start();

            GameClient connector = new GameClient(new TcpClient(ServerConnector.LocalIPAddress().ToString(),180));
            new Thread(() => connector.checkInfinite()).Start();

            GameClient connector2 = new GameClient(new TcpClient(ServerConnector.LocalIPAddress().ToString(), 180));
            new Thread(() => connector2.checkInfinite()).Start();


            new Thread(()=>Application.Run(new GameGUI(connector))).Start();
            new Thread(() => Application.Run(new GameGUI(connector2))).Start();
        }
    }
}
