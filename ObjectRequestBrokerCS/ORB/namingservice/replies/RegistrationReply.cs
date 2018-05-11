using namingservice.replies;

namespace ORB.namingservice.replies
{
	public class RegistrationReply : Reply
	{

		public RegistrationReply(string reply_type, bool request_resolved) : base(reply_type, request_resolved)
		{
		}

		public RegistrationReply()
		{
		}
	}

}