using System;
using System.Reflection;
using ORB.orbapi.proxy;

namespace ORB.orbapi
{
    public class InvokerFunc : IProxyInvocationHandler
    {
        private Func<object, MethodInfo, object[],object> _func;
        public InvokerFunc(Func<object, MethodInfo, object[],object> func)
        {
            this._func = func;
        }

        public object Invoke(object proxy, MethodInfo method, object[] parameters)
        {
            return this._func(proxy,method,parameters);
        }
    }
}