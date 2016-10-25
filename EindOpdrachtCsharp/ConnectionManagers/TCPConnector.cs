using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace EindOpdrachtCsharp.ConnectionManagers
{
    public class TCPConnector
    {
        public delegate void ConnectionError(ErrorLevel errorLevel, string errorMessage, object sender);

        public delegate void DataReceived(object data, object sender);

        public enum ErrorLevel
        {
            SERIALIZATIONERROR,
            SOCKETERROR,
            UKNOWN
        }

        public static ErrorLevel allowedErrorLevel = ErrorLevel.SERIALIZATIONERROR;
        private readonly TcpClient client;
        private bool connected = true;

        public ConnectionError errorNotifier;
        public DataReceived notifyOnData;
        private readonly Stream stream;


        public TCPConnector(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
        }

        public void close()
        {
            connected = false;
            try
            {
                client.Close();
            }
            catch (Exception)
            {
            }
        }

        public virtual void sendMessage(CommandsToSend command, object data)
        {
            sendData(new Message(command, data));
        }

        public virtual void sendData(object data)
        {
            if (!connected) return;
            if (!data.GetType().IsSerializable)
            {
                errorNotifier.Invoke(ErrorLevel.SERIALIZATIONERROR, data + "", this);
                return;
            }
            try

            {
                var bf = new BinaryFormatter();
                bf.Serialize(stream, data);
            }
            catch (Exception e)
            {
                if (errorNotifier == null) return;
                if (e is SerializationException)
                {
                    errorNotifier.Invoke(ErrorLevel.SERIALIZATIONERROR, e.Message, this);
                    return;
                }
                if (e is SocketException)
                {
                    errorNotifier.Invoke(ErrorLevel.SOCKETERROR, e.Message, this);
                    return;
                }
                errorNotifier.Invoke(ErrorLevel.UKNOWN, e.Message, this);
            }
        }

        public object checkForData()
        {
            var bf = new BinaryFormatter();
            try
            {
                return bf.Deserialize(stream);
            }
            catch (Exception e)
            {
                if (errorNotifier == null) return null;
                if (e is SerializationException)
                {
                    errorNotifier.Invoke(ErrorLevel.SERIALIZATIONERROR, e.Message, this);
                    return null;
                }
                if (e is SocketException)
                {
                    errorNotifier.Invoke(ErrorLevel.SOCKETERROR, e.Message, this);
                    return null;
                }
                errorNotifier.Invoke(ErrorLevel.UKNOWN, e.Message, this);
            }
            return null;
        }

        public void checkInfinite()
        {
            while (connected)
            {
                var received = checkForData();
                if (received != null)
                    parseReceivedObject(received);
            }
        }

        public virtual void parseReceivedObject(object obj)
        {
            if (notifyOnData != null)
                notifyOnData.Invoke(obj, this);
        }
    }

    [Serializable]
    public struct Message
    {
        public CommandsToSend command;
        public object data;

        public Message(CommandsToSend command, object data)
        {
            this.command = command;
            this.data = data;
        }
    }
}