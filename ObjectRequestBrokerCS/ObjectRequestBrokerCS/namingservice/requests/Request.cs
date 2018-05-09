namespace namingservice.requests
{
	public class Request
	{
		private string request_type;

		public Request(string request_type)
		{
			this.request_type = request_type;
		}

		public Request()
		{
		}

		public virtual string Request_type
		{
			get
			{
				return request_type;
			}
		}
	}

}