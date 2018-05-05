package namingservice.requests;

import com.google.gson.Gson;

public class Request {
    private String request_type;

    public Request(String request_type) {
        this.request_type = request_type;
    }

    public String getRequest_type() {
        return request_type;
    }

    public void setRequest_type(String request_type) {
        this.request_type = request_type;
    }

    public byte[] getBytes(){
        return new Gson().toJson(this).getBytes();
    }
}
