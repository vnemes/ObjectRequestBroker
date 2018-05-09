package requestreplyapi.entries;

import requestreplyapi.common.IAddress;

public class Entry implements IAddress
{
	private String destinationId;
	private int portNr;
	public Entry(String theDest, int thePort)
	{
		destinationId = theDest;
		portNr = thePort;
	}

	public Entry() {
	}

	public String dest()
	{
		return destinationId;
	}
	public int port()
	{
		return portNr;
	}

    @Override
    public String toString() {
        return "Entry{" +
                "destinationId='" + destinationId + '\'' +
                ", portNr=" + portNr +
                '}';
    }
}