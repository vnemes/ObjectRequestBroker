using System;

namespace requestreplyapi.requestreply
{
    using ObjectRequestBrokerCS.tests;
    using System.Net.Sockets;
    using IAddress = common.IAddress;



    public class Requestor
    {

        private TcpClient client;
        private NetworkStream stream;
        private System.IO.Stream iStr;
        private string myName;

        public Requestor(string theName)
        {
            myName = theName;
        }


        public virtual byte[] deliver_and_wait_feedback(IAddress theDest, byte[] data)
        {

            byte[] buffer = null;
            int val;
            try
            {
                client = new TcpClient(theDest.dest(), theDest.port());
                Log.log("Requestor: Socket" + client.ToString());
                stream = client.GetStream();
                stream.WriteByte((byte)(data.Length >> 8));
                stream.WriteByte((byte)(data.Length & 0xFF));
                stream.Write(data, 0, data.Length);
                stream.Flush();
                val = stream.ReadByte() << 8;
                val |= stream.ReadByte();
                buffer = new byte[val];
                stream.Read(buffer, 0, buffer.Length);
            }
            catch (SocketException)
            {
                Log.log("IOException in deliver_and_wait_feedback");
            }
            finally
            {

                stream.Close();
                client.Close();
            }
            return buffer;
        }

    }


}