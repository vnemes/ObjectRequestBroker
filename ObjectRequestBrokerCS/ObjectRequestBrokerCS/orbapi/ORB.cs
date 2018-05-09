//========================================================================
// This conversion was produced by the Free Edition of
// Java to C# Converter courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

using System;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using namingservice;
using namingservice.replies;
using namingservice.requests;
using orbapi;
using ObjectRequestBrokerCS.vson;
using requestreplyapi.requestreply;
using ObjectRequestBrokerCS.vson;

namespace orbapi
{
	using NamingService = namingservice.NamingService;
	using namingservice.replies;
	using namingservice.requests;
	using Entry = requestreplyapi.entries.Entry;
	using ExtendedEntry = requestreplyapi.entries.ExtendedEntry;
	using requestreplyapi.requestreply;



	public class ORB
	{

		public static object getObjectReference(string objectName, Type interfaceClass)
		{

			Object o = new Object();
			RealProxy<interfaceClass> i;

			InvocationHandler handler = (proxy, method, args) =>
		{
			Requestor req = new Requestor("Requestor");
			byte[] reply = req.deliver_and_wait_feedback(getProviderAddress(objectName),  Marshaller.marshallObject(new MethodCall(method.Name, args)));
			object retval = Marshaller.unMarshallObject(reply);
			Console.WriteLine("@called " + method.Name + " from " + objectName + " and received " + retval.ToString() + " as return value");
			return retval;
		};

			return Proxy.newProxyInstance(interfaceClass.ClassLoader, new Type[]{interfaceClass}, handler);
		}


		public static void register(string name, int port, string entryType, object @object)
		{
				if (!registerToNamingService(name, port, entryType))
				{
					return;
				}

				Replyer replyer = new Replyer(name, new Entry(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), port));

				while (true)
				{
					replyer.receive_transform_and_send_feedback(@in =>
							// fall back here if method arguements are primitives
								// convert parameter classes to primitive classes
				{
					MethodCall methodRequest = (MethodCall) Marshaller.unMarshallObject(@in);
					
						try
						{
							Type[] paramtypes = new Type[methodRequest.Args.Length];
							for (int i = 0; i < methodRequest.Args.Length; i++)
							{
								paramtypes[i] = methodRequest.Args[i].GetType();
							}
							MethodInfo reflMethod = @object.GetType().GetMethod(methodRequest.MethodName, paramtypes);
							return Marshaller.marshallObject(reflMethod.Invoke(@object, methodRequest.Args));
						}
						catch (Exception e)
						{
							Type[] paramtypes = new Type[methodRequest.Args.Length];
							for (int i = 0; i < methodRequest.Args.Length; i++)
							{
								paramtypes[i] = FieldUtils.attemptPrimitiveConvesion(methodRequest.Args[i].GetType());
							}
							MethodInfo reflMethod = @object.GetType().GetMethod(methodRequest.MethodName, paramtypes);
							return Marshaller.marshallObject(reflMethod.Invoke(@object, methodRequest.Args));
						}
					
					
					return null;
				}});

			}

		private static bool registerToNamingService(string name, int port, string entryType)
		{

			Requestor r = new Requestor(name);
			RegistrationRequest rr = new RegistrationRequest(name, new ExtendedEntry(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), port, entryType));
			byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, Marshaller.marshallObject(rr));
			// todo: for deployment, the IP & Port of the naming service shall be retrieved from a configuration file
			RegistrationReply reply = (RegistrationReply) Marshaller.unMarshallObject(resp);
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

		public static ExtendedEntry getProviderAddress(string serviceName)
		{

			Requestor r = new Requestor("getter");
			LocalizationRequest lr = new LocalizationRequest(serviceName);
			byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, Marshaller.marshallObject(lr));
			LocalizationReply reply = (LocalizationReply) Marshaller.unMarshallObject(resp);
			if (reply.Request_resolved)
			{
				Console.WriteLine("@localized " + serviceName + ": " + reply.Entry_data.ToString());
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