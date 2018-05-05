package orbapi;

import com.google.gson.Gson;

import java.lang.reflect.Method;


public class Marshaller {
    private static final Gson gson = new Gson();

    public static byte[] marshallMethod(Method m, Object[] args){

        MethodCall methodCall = new MethodCall(m.getName(),args);
        return gson.toJson(methodCall).getBytes();
    }

    public static MethodCall unmarshallMethod(byte[] req){
         return gson.fromJson(new String(req),MethodCall.class);

    }

    public static byte[] marshallObject(Object obj){
        return gson.toJson(obj).getBytes();
    }

    public static Object unmarshallObject(byte[] reply, Class mClass){
        return gson.fromJson(new String(reply),mClass);
    }
}
