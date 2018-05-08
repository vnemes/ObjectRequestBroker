package orbapi;

import vson.Vson;
import java.lang.reflect.Method;


public class Marshaller {


    public static byte[] marshallMethod(Method m, Object[] args){

        MethodCall methodCall = new MethodCall(m.getName(),args);
        return Vson.toJson(methodCall).getBytes();
    }

    public static MethodCall unMarshallMethod(byte[] req){
         return (MethodCall)Vson.fromJson(new String(req));

    }

    public static byte[] marshallObject(Object obj){
        return Vson.toJson(obj).getBytes();
    }

    public static Object unMarshallObject(byte[] reply){
        return Vson.fromJson(new String(reply));
    }
}
