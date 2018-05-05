package requestreplyapi.examples;

import requestreplyapi.RequestReply.*;
import requestreplyapi.MessageMarshaller.*;
import requestreplyapi.Registry.*;
import requestreplyapi.Commons.IAddress;


public class ClientWithRR
{
	public static void main(String args[])
	{
		new Configuration();

                IAddress dest=Registry.instance().get("Server");

		Message msg= new Message("Client","How are you");

		Requestor r = new Requestor("Client");
		
		Marshaller m = new Marshaller();
			
		byte[] bytes = m.marshal(msg);

		bytes = r.deliver_and_wait_feedback(dest, bytes);
		
		Message answer = m.unmarshal(bytes);

		System.out.println("Client received message "+answer.data+" from "+answer.sender);
	}

}