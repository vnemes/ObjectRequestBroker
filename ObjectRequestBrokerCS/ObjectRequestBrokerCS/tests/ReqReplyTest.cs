using requestreplyapi.common;
using requestreplyapi.entries;
using requestreplyapi.requestreply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectRequestBrokerCS.tests
{
    internal class Transformer : ByteStreamTransformer
    {

        public Transformer() { }

        byte[] ByteStreamTransformer.transform(byte[] @in)
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
