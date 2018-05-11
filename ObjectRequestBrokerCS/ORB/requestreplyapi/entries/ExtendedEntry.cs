namespace ORB.requestreplyapi.entries
{
	public class ExtendedEntry : Entry
	{
		private string entryType;

		public ExtendedEntry(string theDest, int thePort, string entryType) : base(theDest, thePort)
		{
			this.entryType = entryType;
		}

		public ExtendedEntry()
		{
		}

		public virtual string Type()
		{
			return entryType;
		}

		public override string ToString()
		{
			return "ExtendedEntry{" +
					"entryType='" + entryType + '\'' +
					base.ToString() + '}';
		}
	}

}