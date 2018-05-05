package namingservice.requests;

import com.google.gson.Gson;
import requestreplyapi.Registry.ExtendedEntry;

public class RegistrationRequest extends Request{
    private ExtendedEntry entry_data;
    private String entry_name;

    public RegistrationRequest(String entry_name, ExtendedEntry entry_data) {
        super("registration_request");
        this.entry_data = entry_data;
        this.entry_name = entry_name;
    }

    public ExtendedEntry getEntry_data() {
        return entry_data;
    }

    public void setEntry_data(ExtendedEntry entry_data) {
        this.entry_data = entry_data;
    }

    public String getEntry_name() {
        return entry_name;
    }

    public void setEntry_name(String entry_name) {
        this.entry_name = entry_name;
    }


}
