using System;
using ObjectRequestBrokerCS.vson;

namespace orbapi
{


	public class Marshaller
	{

		public static byte[] marshallObject(object obj)
		{
			return toByteArray(Vson.toJson(obj));
		}

		public static object unMarshallObject(byte[] reply)
		{
			return Vson.fromJson(stringFromByteArray(reply));
		}

		private static byte[] toByteArray(String str)
		{
			return System.Text.Encoding.ASCII.GetBytes(str);
		}

		public static String stringFromByteArray(byte[] bytes)
		{
			return System.Text.Encoding.ASCII.GetString(bytes);
		}
	}

}