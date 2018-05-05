package requestreplyapi.MessageMarshaller;

import java.lang.String;


public class Marshaller
{
	public byte[] marshal(Message theMsg)
	{
		String m = "  " + theMsg.sender + ":" + theMsg.data;
		byte b[] = new byte[m.length()];
		b = m.getBytes();
		b[0] = (byte)m.length();
		return b;
	}
	public Message unmarshal(byte[] byteArray)
	{
		String msg = new String(byteArray);
		String sender = msg.substring(1, msg.indexOf(":"));
		String m = msg.substring(msg.indexOf(":")+1, msg.length()-1);
		return new Message(sender, m);
	}

}





