namespace namingservice.requests
{
	using ExtendedEntry = requestreplyapi.entries.ExtendedEntry;

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

		public virtual ExtendedEntry Entry_data
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


		public virtual string Entry_name
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