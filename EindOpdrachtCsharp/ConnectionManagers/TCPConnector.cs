using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtCsharp.ConnectionManagers
{
    public class TCPConnector
    {
        private Stream stream;
        private TcpClient client;
        private bool connected = true;
        public TCPConnector(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
        }

        public virtual void sendData(object data)
        {
            if (!data.GetType().IsSerializable) return;
            try

            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, data);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                Console.Beep(100,1);
            }
        }

        public object checkForData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                return bf.Deserialize(stream);
            }
            catch (Exception e)
            {
                //Console.Error.WriteLine(e);
            }
            return null;
        }

        public void checkInfinite()
        {
            while (connected)
            {
                object received = checkForData();
                if(received != null)
                    parseReceivedObject(received);
            }
        }

        public virtual void parseReceivedObject(object obj)
        {
        }

    }
}
