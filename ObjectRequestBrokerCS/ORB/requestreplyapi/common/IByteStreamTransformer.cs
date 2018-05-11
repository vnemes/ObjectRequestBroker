namespace ORB.requestreplyapi.common
{
	public interface IByteStreamTransformer
	{
		byte[] Transform(byte[] @in);
	}
}