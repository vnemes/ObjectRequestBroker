using System.Threading;

namespace ServerExecutable
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
//            new Thread(() =>
//            {
//                Thread.CurrentThread.IsBackground = true;
//
//                MathServer.start();
//            }).Start();
//            Thread.Sleep(50);
//            InfoServer.start();
            MathServer.start();
        }
    }
}