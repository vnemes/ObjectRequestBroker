package namingservice;

import orbapi.Marshaller;
import requestreplyapi.entries.Entry;
import requestreplyapi.entries.ExtendedEntry;
import requestreplyapi.requestreply.Replyer;
import namingservice.replies.InvalidRequestReply;
import namingservice.replies.LocalizationReply;
import namingservice.replies.RegistrationReply;
import namingservice.replies.Reply;
import namingservice.requests.LocalizationRequest;
import namingservice.requests.RegistrationRequest;

import java.net.Inet4Address;
import java.net.UnknownHostException;
import java.util.HashMap;

public class NamingService {
    private static NamingService instance = new NamingService();
    public static Entry NAMING_SERVICE_ENTRY;

    static {
        try {
            NAMING_SERVICE_ENTRY = new Entry(Inet4Address.getLocalHost().getHostAddress(), 1111);
        } catch (UnknownHostException e) {
            e.printStackTrace();
            NAMING_SERVICE_ENTRY = null;
        }
    }

    private static final String NAMING_SERVICE_NAME = "NamingService";
    private HashMap<String, ExtendedEntry> entryMap;
    private boolean isServiceRunning;

    public static NamingService getInstance(){
        return instance;
    }

    private NamingService() {
        entryMap = new HashMap<>();
        isServiceRunning =false;
    }

    public void startService() {

        if (isServiceRunning) {
            System.out.println("NamingService already running");
            return;
        }

        Replyer mReplyer = new Replyer(NAMING_SERVICE_NAME, NAMING_SERVICE_ENTRY);
        isServiceRunning = true;

        while (isServiceRunning)
            mReplyer.receive_transform_and_send_feedback(in -> {

                Reply reply;
                String inStr = new String(in);
                if (inStr.contains("registration_request"))
                        reply = processRegistrationRequest(in);
                else if (inStr.contains("localization_request"))
                        reply = processLocalizationRequest(in);
                else reply = new InvalidRequestReply();
                return Marshaller.marshallObject(reply);
            });
    }

    private Reply processLocalizationRequest(byte[] in) {
        Reply reply;LocalizationRequest locRequest = (LocalizationRequest) Marshaller.unMarshallObject(in);

        reply = new LocalizationReply("localization_reply", entryMap.get(locRequest.getEntry_name()), entryMap.get(locRequest.getEntry_name()) != null);
        return reply;
    }

    private Reply processRegistrationRequest(byte[] in) {
        Reply reply;RegistrationRequest regRequest = (RegistrationRequest) Marshaller.unMarshallObject(in);
        boolean req_res;
        if (entryMap.containsKey(regRequest.getEntry_name())){
            req_res = false;
        } else{
            entryMap.put(regRequest.getEntry_name(), regRequest.getEntry_data());
            req_res = true;
        }
        reply = new RegistrationReply("registration_reply", req_res);
        return reply;
    }

    public void stopService(){
        if (!isServiceRunning){
            System.out.println("NamingService is not running!");
            return;
        }
        isServiceRunning = false;
    }

    public static void main(String[] args) {
        getInstance().startService();
    }
}
