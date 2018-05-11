package ORB.namingservice.replies;


public class Reply {
    private String reply_type;
    private boolean request_resolved;

    public Reply(String reply_type, boolean request_resolved) {
        this.reply_type = reply_type;
        this.request_resolved = request_resolved;
    }

    public Reply() {
    }

    public String getReply_type() {
        return reply_type;
    }

    public boolean isRequest_resolved() {
        return request_resolved;
    }
}
