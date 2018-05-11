namespace namingservice.replies
{

	public class Reply
	{
		private string reply_type;
		private bool request_resolved;

		public Reply(string reply_type, bool request_resolved)
		{
			this.reply_type = reply_type;
			this.request_resolved = request_resolved;
		}

		public Reply()
		{
		}

		public virtual string Reply_type
		{
			get
			{
				return reply_type;
			}
		}

		public virtual bool Request_resolved
		{
			get
			{
				return request_resolved;
			}
		}
	}

}