package requestreplyapi.Registry;

import java.util.Hashtable;

public class Registry
{
	private Hashtable hTable = new Hashtable();
	private static Registry _instance = null;

	private Registry() { }

	public static Registry instance()
	{
		if (_instance == null)
			_instance = new Registry();
		return _instance;
	}

	public void put(String theKey, Entry theEntry)
	{
		hTable.put(theKey, theEntry);
	}
	public Entry get(String aKey)
	{
		return (Entry)hTable.get(aKey);
	}
}




