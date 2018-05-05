package namingservice;

import orbapi.Marshaller;
import requestreplyapi.entries.Entry;
import requestreplyapi.entries.ExtendedEntry;
import requestreplyapi.requestreply.Replyer;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
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
    private Gson gson;
    private HashMap<String, ExtendedEntry> entryMap;

    public NamingService() {
        entryMap = new HashMap<>();
        gson = new Gson();
    }

    public void startService() {
        Replyer mReplyer = new Replyer(NAMING_SERVICE_NAME, NAMING_SERVICE_ENTRY);

        while (true)
            mReplyer.receive_transform_and_send_feedback(in -> {

                Reply reply;
                String inStr = new String(in);
                JsonObject object = new JsonParser().parse(inStr).getAsJsonObject();

                switch (object.get("request_type").toString().replaceAll("^\"|\"$", "")) {
                    case "registration_request":
                        reply = processRegistrationRequest(inStr);
                        break;

                    case "localization_request":
                        reply = processLocalizationRequest(inStr);
                        break;

                    default:
                        reply = new InvalidRequestReply();
                        break;
                }
                return Marshaller.marshallObject(reply);
            });
    }

    private Reply processLocalizationRequest(String inStr) {
        Reply reply;LocalizationRequest locRequest = gson.fromJson(inStr, LocalizationRequest.class);

        reply = new LocalizationReply("localization_reply", entryMap.get(locRequest.getEntry_name()), entryMap.get(locRequest.getEntry_name()) != null);
        return reply;
    }

    private Reply processRegistrationRequest(String inStr) {
        Reply reply;RegistrationRequest regRequest = gson.fromJson(inStr, RegistrationRequest.class);
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

    public static void main(String[] args) {
        NamingService namingService = new NamingService();
        namingService.startService();
    }
}
