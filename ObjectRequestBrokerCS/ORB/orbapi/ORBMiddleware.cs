using System;
using System.Net;
using ORB.namingservice;
using ORB.namingservice.replies;
using ORB.namingservice.requests;
using ORB.orbapi.proxy;
using ORB.requestreplyapi.entries;
using ORB.requestreplyapi.requestreply;

namespace ORB.orbapi
{
    using NamingService = NamingService;
    using Entry = Entry;
    using ExtendedEntry = ExtendedEntry;


    public static class ORBMiddleware
    {
        private static Entry _namingServiceAddress = NamingService.NAMING_SERVICE_ENTRY;

        public static object GetObjectReference(string objectName, Type interfaceType)
        {
            return ProxyFactory.GetInstance().Create(new InvokerFunc((proxy, method, args) =>
                {
                    var req = new Requestor("Requestor");
                    var reply = req.deliver_and_wait_feedback(GetProviderAddress(objectName),
                        Marshaller.MarshallObject(new MethodCall(method.Name, args)));
                    var retval = Marshaller.UnMarshallObject(reply);
                    Console.WriteLine("@called " + method.Name + " from " + objectName + " and received " +
                                      retval.ToString() + " as return value");
                    return retval;
                }
            ), interfaceType, true);
        }


        public static void Register(string name, int port, string entryType, object @object)
        {
            if (!RegisterToNamingService(name, port, entryType))
            {
                return;
            }

            var replyer = new Replyer(name,
                new Entry(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), port));


            while (true)
            {
                replyer.receive_transform_and_send_feedback(new TransformerFunc((@in) =>
                {
                    var methodRequest = (MethodCall) Marshaller.UnMarshallObject(@in);

                    var paramtypes = new Type[methodRequest.Args.Length];
                    for (var i = 0; i < methodRequest.Args.Length; i++)
                    {
                        paramtypes[i] = methodRequest.Args[i].GetType();
                    }

                    var reflMethod = @object.GetType().GetMethod(methodRequest.MethodName, paramtypes);
                    return Marshaller.MarshallObject(reflMethod.Invoke(@object, methodRequest.Args));
                }));
            }
        }

        public static bool RegisterToNamingService(string name, int port, string entryType)
        {
            var r = new Requestor(name);
            var rr = new RegistrationRequest(name,
                new ExtendedEntry(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), port, entryType));
            var resp =
                r.deliver_and_wait_feedback(_namingServiceAddress, Marshaller.MarshallObject(rr));
            // todo: for deployment, the IP & Port of the naming service shall be retrieved from a configuration file
            var reply = (RegistrationReply) Marshaller.UnMarshallObject(resp);
            if (reply.Request_resolved)
            {
                Console.WriteLine("@registered " + name);
            }
            else
            {
                Console.WriteLine("@cannot register " + name);
                return false;
            }

            return true;
        }

        public static void ChangeNamingServiceAddress(string address, int port)
        {
            _namingServiceAddress = new Entry(address, port);
        }

        public static ExtendedEntry GetProviderAddress(string serviceName)
        {
            var r = new Requestor("getter");
            var lr = new LocalizationRequest(serviceName);
            var resp =
                r.deliver_and_wait_feedback(_namingServiceAddress, Marshaller.MarshallObject(lr));
            var reply = (LocalizationReply) Marshaller.UnMarshallObject(resp);
            if (reply.Request_resolved)
            {
                Console.WriteLine("@localized " + serviceName + ": " + reply.Entry_data);
            }
            else
            {
                Console.WriteLine("@cannot localize " + serviceName);
                return null;
            }

            return reply.Entry_data;
        }
    }
}