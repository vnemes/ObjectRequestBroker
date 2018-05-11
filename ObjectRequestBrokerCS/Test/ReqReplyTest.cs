using System;
using System.Text;
using System.Threading;
using ORB.requestreplyapi.common;
using ORB.requestreplyapi.entries;
using ORB.requestreplyapi.requestreply;

namespace Test
{
    internal class Transformer : IByteStreamTransformer
    {

        public Transformer() { }

        byte[] IByteStreamTransformer.Transform(byte[] @in)
        {
            return Encoding.ASCII.GetBytes("Bye!");
        }
        
    }

    internal static class ReqReplyTest
    {



        public static void test()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                var rr = new Replyer("name2", new Entry("127.0.0.1", 1000));
                rr.receive_transform_and_send_feedback(new Transformer());
            }).Start();
            var r = new Requestor("name");
            var recv = r.deliver_and_wait_feedback(new Entry("127.0.0.1", 1000), Encoding.ASCII.GetBytes("Hello"));
            Console.WriteLine(Encoding.ASCII.GetString(recv, 0, recv.Length));
        }
    }
}
