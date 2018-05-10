using System;
using System.Threading;
using namingservice;
using orbapi;
using requestreplyapi.entries;

namespace ObjectRequestBrokerCS.tests
{
    public class NamingServiceTest
    {
        public static void test()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                NamingService.Instance.startService();
            }).Start();

            ORB.registerToNamingService("Test", 1324, "TestType");
            Log.log(ORB.getProviderAddress("Test").ToString());
        }
    }
}