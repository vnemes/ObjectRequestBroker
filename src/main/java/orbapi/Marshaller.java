package orbapi;

import com.google.gson.Gson;

import java.lang.reflect.Method;
import java.util.HashMap;
import java.util.Map;


public class Marshaller {
    private static final Gson gson = new Gson();

    private static Map<Class,Class> builtInMap = new HashMap<>();

    static {
        builtInMap.put(Integer.class, Integer.TYPE );
        builtInMap.put(Long.class, Long.TYPE );
        builtInMap.put(Double.class, Double.TYPE );
        builtInMap.put(Float.class, Float.TYPE );
        builtInMap.put(Boolean.class, Boolean.TYPE );
        builtInMap.put(Character.class, Character.TYPE );
        builtInMap.put(Byte.class, Byte.TYPE );
        builtInMap.put(Void.class, Void.TYPE );
        builtInMap.put(Short.class, Short.TYPE );
    }

    public static Class attemptPrimitiveConvesion(Class mClass){
        return builtInMap.getOrDefault(mClass, mClass);
    }

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
