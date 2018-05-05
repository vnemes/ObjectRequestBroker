package namingservice;

import requestreplyapi.Registry.Entry;
import requestreplyapi.Registry.ExtendedEntry;
import requestreplyapi.RequestReply.Replyer;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import namingservice.replies.InvalidRequestReply;
import namingservice.replies.LocalizationReply;
import namingservice.replies.RegistrationReply;
import namingservice.replies.Reply;
import namingservice.requests.LocalizationRequest;
import namingservice.requests.RegistrationRequest;

import java.util.HashMap;

public class NamingService {
    public static final Entry NAMING_SERVICE_ENTRY = new Entry("127.0.0.1", 1111);
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
                return gson.toJson(reply).getBytes();
            });
    }

    private Reply processLocalizationRequest(String inStr) {
        Reply reply;LocalizationRequest locRequest = gson.fromJson(inStr, LocalizationRequest.class);

        reply = new LocalizationReply("localization_reply", entryMap.get(locRequest.getEntry_name()), true);
        return reply;
    }

    private Reply processRegistrationRequest(String inStr) {
        Reply reply;RegistrationRequest regRequest = gson.fromJson(inStr, RegistrationRequest.class);
        entryMap.put(regRequest.getEntry_name(), regRequest.getEntry_data());

        reply = new RegistrationReply("registration_reply", true);
        return reply;
    }

    public static void main(String[] args) {
        NamingService namingService = new NamingService();
        namingService.startService();
    }
}
