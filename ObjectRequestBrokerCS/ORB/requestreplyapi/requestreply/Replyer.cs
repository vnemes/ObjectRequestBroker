using System;
using System.Net.Sockets;
using ORB.requestreplyapi.common;

namespace ORB.requestreplyapi.requestreply
{
    using IAddress = IAddress;


    public class Replyer
    {
        private TcpListener _server;
        private TcpClient _client;
        private NetworkStream _stream;
        private string _myName;
        private IAddress _myAddr;

        public Replyer(string theName, IAddress theAddr)
        {
            _myName = theName;
            _myAddr = theAddr;
        }


        public virtual void receive_transform_and_send_feedback(IByteStreamTransformer t)
        {
            try
            {
                _server = new TcpListener(_myAddr.Port());
                Console.WriteLine("Replyer TCPListener on port {0}", _myAddr.Port());

                _server.Start();
                _client = _server.AcceptTcpClient();
                Console.WriteLine("Replyer accept: TCPClient {0}:{1}", _myAddr.Dest(),_myAddr.Port());
                _stream = _client.GetStream();
                //correction performed for objects bigger than 256 bytes
                var val = _stream.ReadByte() << 8;
                val |= _stream.ReadByte();
                var buf = new byte[val];
                _stream.Read(buf, 0, val);
                byte[] data = t.Transform(buf);
                _stream.WriteByte((byte) (data.Length >> 8));
                _stream.WriteByte((byte) (data.Length & 0xFF));
                _stream.Write(data, 0, data.Length);
                _stream.Flush();
            }
            catch (SocketException)
            {
                
                Console.WriteLine("IOException in receive_transform_and_feedback");
            }
            finally
            {
                _stream.Close();
                _client.Close();
                _server.Stop();
            }
        }
    }
}