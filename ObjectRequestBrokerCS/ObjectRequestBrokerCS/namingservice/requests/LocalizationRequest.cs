namespace namingservice.requests
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