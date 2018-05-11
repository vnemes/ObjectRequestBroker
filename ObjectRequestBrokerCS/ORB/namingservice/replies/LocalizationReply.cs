using namingservice.replies;
using ORB.requestreplyapi.entries;

namespace ORB.namingservice.replies
{
	using ExtendedEntry = ExtendedEntry;

	public class LocalizationReply : Reply
	{
		private ExtendedEntry entry_data;

		public LocalizationReply(string reply_type, ExtendedEntry entry_data, bool request_resolved) : base(reply_type, request_resolved)
		{
			this.entry_data = entry_data;
		}

		public LocalizationReply()
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


		public override string ToString()
		{
			return "LocalizationReply{" +
					"entry_data=" + entry_data +
					'}';
		}
	}

}