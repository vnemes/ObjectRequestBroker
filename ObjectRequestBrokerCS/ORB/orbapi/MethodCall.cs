namespace ORB.orbapi
{

	public class MethodCall
	{
		private string _methodName;
		private object[] _args;

		public MethodCall()
		{
		}

		internal MethodCall(string methodName, object[] args)
		{
			_methodName = methodName;
			_args = args;
		}

		public virtual string MethodName
		{
			get
			{
				return _methodName;
			}
		}


		public virtual object[] Args
		{
			get
			{
				return _args;
			}
		}

		public override string ToString()
		{
			return "MethodCall{" +
					"methodName='" + _methodName + '\'' +
					", args=" + _args +
					'}';
		}
	}
}