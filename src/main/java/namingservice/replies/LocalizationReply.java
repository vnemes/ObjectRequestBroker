package namingservice.replies;

import requestreplyapi.entries.ExtendedEntry;

public class LocalizationReply extends Reply {
    private ExtendedEntry entry_data;

    public LocalizationReply(String reply_type, ExtendedEntry entry_data, boolean request_resolved) {
        super(reply_type, request_resolved);
        this.entry_data = entry_data;
    }

    public ExtendedEntry getEntry_data() {
        return entry_data;
    }

    public void setEntry_data(ExtendedEntry entry_data) {
        this.entry_data = entry_data;
    }

    @Override
    public String toString() {
        return "LocalizationReply{" +
                "entry_data=" + entry_data +
                '}';
    }
}
