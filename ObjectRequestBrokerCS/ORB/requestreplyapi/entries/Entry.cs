namespace ORB.requestreplyapi.entries
{

	public class Entry : common.IAddress
	{
		private string destinationId;
		private int portNr;
		public Entry(string theDest, int thePort)
		{
			destinationId = theDest;
			portNr = thePort;
		}

		public Entry()
		{
		}

		public virtual string Dest()
		{
			return destinationId;
		}
		public virtual int Port()
		{
			return portNr;
		}

		public override string ToString()
		{
			return "Entry{" +
					"destinationId='" + destinationId + '\'' +
					", portNr=" + portNr +
					'}';
		}
	}
}