using namingservice;
using ORB.namingservice;

namespace NamingServiceExecutable
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            NamingService.Instance.StartService();
        }
    }
}