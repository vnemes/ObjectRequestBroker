package requestreplyapi.Registry;

import requestreplyapi.Commons.IAddress;

public class Entry implements IAddress
{
	private String destinationId;
	private int portNr;
	public Entry(String theDest, int thePort)
	{
		destinationId = theDest;
		portNr = thePort;
	}
	public String dest()
	{
		return destinationId;
	}
	public int port()
	{
		return portNr;
	}
}