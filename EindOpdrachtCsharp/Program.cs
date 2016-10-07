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

            new Thread(() => new TCPConnector(new TcpClient("127.0.0.1",180)));

        }
    }
}
