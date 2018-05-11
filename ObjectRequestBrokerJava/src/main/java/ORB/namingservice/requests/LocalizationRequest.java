package ORB.namingservice.requests;

public class LocalizationRequest extends  Request{
    private String entry_name;

    public LocalizationRequest(String entry_name) {
        super("localization_request");
        this.entry_name = entry_name;
    }

    public LocalizationRequest() {
    }

    public String getEntry_name() {
        return entry_name;
    }

    public void setEntry_name(String entry_name) {
        this.entry_name = entry_name;
    }


}
