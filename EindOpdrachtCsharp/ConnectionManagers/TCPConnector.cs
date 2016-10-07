using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EindOpdrachtCsharp.ConnectionManagers
{
    public class TCPConnector
    {
        private Stream stream;
        private TcpClient client;
        private bool connected = true;

        public delegate void DataReceived(object data, object sender);
        public DataReceived notifyOnData;

        public TCPConnector(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
        }

        public virtual void sendMessage(CommandsToSend command, object data)
        {
            sendData(new message(command,data));
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
                Console.WriteLine(e);
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
            if(notifyOnData != null)
                notifyOnData.Invoke(obj,this);
        }

    }
    [Serializable]
    public struct message
    {
        public CommandsToSend command;
        public object data;

        public message(CommandsToSend command, object data)
        {
            this.command = command;
            this.data = data;
        }
    }
}
