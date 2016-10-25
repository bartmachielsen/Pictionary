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
    public class Program
    {
        static void Main(string[] args)
        {

            new Thread(()=> new GameConnector(180)).Start();
            for (int i = 0; i < 3; i++)
                startClient();
            /*for (int i = 0; i < 3; i++)
            {
                GameClient connector = new GameClient(new TcpClient(ServerConnector.LocalIPAddress().ToString(), 180));
                new Thread(() => connector.checkInfinite()).Start();

                new Thread(() =>
                {
                    Application.EnableVisualStyles();
                    Application.Run(new GameGUI(connector));
                }).Start();
            }*/
        }

        public static void startClient()
        {
            GameClient connector = new GameClient(new TcpClient(ServerConnector.LocalIPAddress().ToString(), 180));
            new Thread(() => connector.checkInfinite()).Start();

            new Thread(() =>
            {
                Application.EnableVisualStyles();
                Application.Run(new GameGUI(connector));
            }).Start();
        }
    }
}
