using ORB.orbapi;

namespace ClientExecutable
{
    public class MathClient
    {
        public static void start()
        {
            var math = (IMath) ORBMiddleware.GetObjectReference("MathServer", typeof(IMath));
            math.do_add(2.5, 1.4);
            math.do_sqr(16.1);
        }
    }
}