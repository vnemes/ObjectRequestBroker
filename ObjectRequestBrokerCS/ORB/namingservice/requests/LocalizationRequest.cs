using namingservice.requests;

namespace ORB.namingservice.requests
{
	public class LocalizationRequest : Request
	{
		private string entry_name;

		public LocalizationRequest(string entry_name) : base("localization_request")
		{
			this.entry_name = entry_name;
		}

		public LocalizationRequest()
		{
		}

		public virtual string EntryName
		{
			get
			{
				return entry_name;
			}
			set
			{
				entry_name = value;
			}
		}



	}

}