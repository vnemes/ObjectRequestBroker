package orbapi;

import vson.Vson;
import java.lang.reflect.Method;


public class Marshaller {

    public static byte[] marshallObject(Object obj){
        return Vson.toJson(obj).getBytes();
    }

    public static Object unMarshallObject(byte[] reply){
        return Vson.fromJson(new String(reply));
    }
}
