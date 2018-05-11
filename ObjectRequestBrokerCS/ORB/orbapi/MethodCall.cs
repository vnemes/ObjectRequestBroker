namespace ORB.orbapi
{

	public class MethodCall
	{
		private string methodName;
		private object[] args;

		public MethodCall()
		{
		}

		internal MethodCall(string methodName, object[] args)
		{
			this.methodName = methodName;
			this.args = args;
		}

		public virtual string MethodName
		{
			get
			{
				return methodName;
			}
		}


		public virtual object[] Args
		{
			get
			{
				return args;
			}
		}

		public override string ToString()
		{
			return "MethodCall{" +
					"methodName='" + methodName + '\'' +
					", args=" + args +
					'}';
		}
	}
}