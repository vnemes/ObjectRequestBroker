using System;
using System.Threading;
using namingservice;
using ORB.namingservice;
using ORB.orbapi;

namespace Test
{
    public class NamingServiceTest
    {
        public static void test()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                NamingService.Instance.StartService();
            }).Start();

            ORBMiddleware.RegisterToNamingService("Test", 1324, "TestType");
            Console.WriteLine(ORBMiddleware.GetProviderAddress("Test").ToString());
        }
    }
}