package orbapi;

import com.google.gson.Gson;
import namingservice.NamingService;
import namingservice.replies.LocalizationReply;
import namingservice.replies.RegistrationReply;
import namingservice.requests.LocalizationRequest;
import namingservice.requests.RegistrationRequest;
import requestreplyapi.Registry.ExtendedEntry;
import requestreplyapi.RequestReply.Requestor;

import java.net.Inet4Address;
import java.net.UnknownHostException;


public class ORB {
    private static Gson gson = new Gson();

    public static void registerToNamingService(String name, int port, String entryType) {
        Requestor r = new Requestor(name);

        try {
            RegistrationRequest rr = new RegistrationRequest(name, new ExtendedEntry(Inet4Address.getLocalHost().getHostAddress(), port, entryType));
            byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, rr.getBytes());
            RegistrationReply reply = gson.fromJson(new String(resp), RegistrationReply.class);
            System.out.println("@registered " + name);

        } catch (UnknownHostException e) {
            e.printStackTrace();
        }
    }

    public static ExtendedEntry getProviderAddress(String serviceName) {

        Requestor r = new Requestor("getter");
        LocalizationRequest lr = new LocalizationRequest(serviceName);

        byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, lr.getBytes());
        LocalizationReply reply = gson.fromJson(new String(resp), LocalizationReply.class);
        System.out.println("@localized " + serviceName + ": " + reply.getEntry_data().toString());

        return reply.getEntry_data();
    }
}
