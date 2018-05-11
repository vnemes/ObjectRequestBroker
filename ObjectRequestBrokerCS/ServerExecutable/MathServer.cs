using System;
using ORB.orbapi;

namespace ServerExecutable
{
    public class MathServer:IMath
    {
        private static readonly int MATH_SERVER_PORT = 1010;
        private static readonly string MATH_SERVER_NAME = "MathServer";

        
        public double do_add(double a, double b) {
            return a+b;
        }

        
        public double do_sqr(double a) {
            return Math.Sqrt(a);
        }
        
        
        public static void start(){
            var m = new MathServer();
            ORBMiddleware.Register(MATH_SERVER_NAME,MATH_SERVER_PORT,"math",m);
        }
    }
    

}