using System;

namespace requestreplyapi.requestreply
{
    using ObjectRequestBrokerCS.tests;
    using System.Net.Sockets;
    using ByteStreamTransformer = common.ByteStreamTransformer;
    using IAddress = common.IAddress;


    public class Replyer
	{
		private TcpListener server;
		private TcpClient client;
        private NetworkStream stream;
        private System.IO.Stream oStr;
		private string myName;
		private IAddress myAddr;

		public Replyer(string theName, IAddress theAddr)
		{
			myName = theName;
			myAddr = theAddr;
			try
			{
				server = new TcpListener(myAddr.port());
				Log.log("Replyer Serversocket:" + server.ToString());
			}
			catch (Exception)
			{
				Log.log("Error opening server socket");
			}
		}


		public virtual void receive_transform_and_send_feedback(ByteStreamTransformer t)
		{
			int val;
            byte[] buf = null;
			try
			{
                server.Start();
				client = server.AcceptTcpClient();
				Log.log("Replyer accept: Socket" + client.ToString());
				stream = client.GetStream();
                //correction performed for objects bigger than 256 bytes
				val = stream.ReadByte()<< 8;
				val |= stream.ReadByte();
				buf = new byte[val];
				stream.Read(buf, 0, val);
                byte[] data = t.transform(buf);
                stream.WriteByte((byte)(data.Length >> 8));
                stream.WriteByte((byte)(data.Length & 0xFF));
                stream.Write(data, 0, data.Length);
                stream.Flush();

			}
			catch (SocketException)
			{
				Log.log("IOException in receive_transform_and_feedback");
			}
            finally
            {

                stream.Close();
                client.Close();
                server.Stop();
            }

		}

	}


}