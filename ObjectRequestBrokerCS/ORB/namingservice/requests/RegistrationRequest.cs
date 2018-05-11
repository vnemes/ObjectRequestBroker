using namingservice.requests;
using ORB.requestreplyapi.entries;

namespace ORB.namingservice.requests
{
	using ExtendedEntry = ExtendedEntry;

	public class RegistrationRequest : Request
	{
		private ExtendedEntry entry_data;
		private string entry_name;

		public RegistrationRequest(string entry_name, ExtendedEntry entry_data) : base("registration_request")
		{
			this.entry_data = entry_data;
			this.entry_name = entry_name;
		}

		public RegistrationRequest()
		{
		}

		public virtual ExtendedEntry EntryData
		{
			get
			{
				return entry_data;
			}
			set
			{
				this.entry_data = value;
			}
		}


		public virtual string EntryName
		{
			get
			{
				return entry_name;
			}
			set
			{
				this.entry_name = value;
			}
		}



	}

}