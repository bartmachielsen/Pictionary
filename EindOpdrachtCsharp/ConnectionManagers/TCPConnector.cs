using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtCsharp.ConnectionManagers
{
    class TCPConnector
    {
        private Stream stream;

        public TCPConnector(TcpClient client)
        {
            stream = client.GetStream();
        }

        public void sendData(object data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, data);
        }

        public object checkForData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream);
        }
    }
}
