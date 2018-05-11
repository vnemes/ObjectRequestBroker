using ORB.vson;

namespace ORB.orbapi
{


	public static class Marshaller
	{

		public static byte[] MarshallObject(object obj)
		{
			return ToByteArray(Vson.ToJson(obj));
		}

		public static object UnMarshallObject(byte[] reply)
		{
			return Vson.FromJson(StringFromByteArray(reply));
		}

		private static byte[] ToByteArray(string str)
		{
			return System.Text.Encoding.ASCII.GetBytes(str);
		}

		public static string StringFromByteArray(byte[] bytes)
		{
			return System.Text.Encoding.ASCII.GetString(bytes);
		}
	}

}