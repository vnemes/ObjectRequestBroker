package requestreplyapi.examples;

import requestreplyapi.Registry.*;


class Configuration
{
	public Configuration()
	{
		Entry entrys = new Entry("127.0.0.1", 1111);
		Registry.instance().put("Server", entrys);
		
		Entry entryc1 = new Entry("127.0.0.1", 1112);
		Registry.instance().put("Client1", entryc1);
		
		Entry entryc2 = new Entry("127.0.0.1", 1113);
		Registry.instance().put("Client2", entryc2);

	}
}