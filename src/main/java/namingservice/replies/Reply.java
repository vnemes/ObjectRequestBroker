package namingservice.replies;

import com.google.gson.Gson;

public class Reply {
    private String reply_type;
    private boolean request_resolved;

    public Reply(String reply_type, boolean request_resolved) {
        this.reply_type = reply_type;
        this.request_resolved = request_resolved;
    }

    public String getReply_type() {
        return reply_type;
    }

    public void setReply_type(String reply_type) {
        this.reply_type = reply_type;
    }

    public boolean isRequest_resolved() {
        return request_resolved;
    }

    public void setRequest_resolved(boolean request_resolved) {
        this.request_resolved = request_resolved;
    }

    public byte[] getBytes(){
        return new Gson().toJson(this).getBytes();
    }
}
