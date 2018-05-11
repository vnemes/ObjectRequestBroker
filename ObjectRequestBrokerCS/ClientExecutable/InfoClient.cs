using ORB.orbapi;

namespace ClientExecutable
{
    public class InfoClient
    {
        public static void start()
        {
            var info = (IInfo) ORBMiddleware.GetObjectReference("InfoServer", typeof(IInfo));
            info.get_road_info(66);
            info.get_temp("Timisoara");
        }
    }
}