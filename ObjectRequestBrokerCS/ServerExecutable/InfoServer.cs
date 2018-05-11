using System;
using ORB.orbapi;

namespace ServerExecutable
{
    public class InfoServer:IInfo
    {
        private static readonly string INFO_SERVER_NAME = "InfoServer";
        private static readonly int INFO_SERVER_PORT = 2020;


        public string get_road_info(long road_ID)
        {
            return "Informations about road " + road_ID;
        }


        public double get_temp(string city)
        {
            return new Random().NextDouble();
        }

        public static void start()
        {
            var infoServer = new InfoServer();
            ORBMiddleware.Register(INFO_SERVER_NAME, INFO_SERVER_PORT, "info", infoServer);
        }
    }
}