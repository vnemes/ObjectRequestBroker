namespace requestreplyapi.common
{
	public interface ByteStreamTransformer
	{
		byte[] transform(byte[] @in);
	}
}