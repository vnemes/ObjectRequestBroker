using System;
using System.Net.Sockets;

namespace ORB.requestreplyapi.requestreply
{
    using IAddress = common.IAddress;



    public class Requestor
    {

        private TcpClient _client;
        private NetworkStream _stream;
        private string _myName;

        public Requestor(string theName)
        {
            _myName = theName;
        }


        public virtual byte[] deliver_and_wait_feedback(IAddress theDest, byte[] data)
        {

            byte[] buffer = null;
            try
            {
                _client = new TcpClient(theDest.Dest(), theDest.Port());
                Console.WriteLine("Requestor: TCPClient {0}:{1}", theDest.Dest(),theDest.Port());
                _stream = _client.GetStream();
                _stream.WriteByte((byte)(data.Length >> 8));
                _stream.WriteByte((byte)(data.Length & 0xFF));
                _stream.Write(data, 0, data.Length);
                _stream.Flush();
                var val = _stream.ReadByte() << 8;
                val |= _stream.ReadByte();
                buffer = new byte[val];
                _stream.Read(buffer, 0, buffer.Length);
            }
            catch (SocketException)
            {
                Console.WriteLine("IOException in deliver_and_wait_feedback");
            }
            finally
            {

                _stream.Close();
                _client.Close();
            }
            return buffer;
        }

    }


}