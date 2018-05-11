package ORB.vson;

import java.lang.reflect.Field;
import java.util.*;
import java.util.stream.Stream;

public class FieldUtils {

    private static final Set<Class> WRAPPER_PRIMITIVES = new HashSet<>(Arrays.asList(
            Boolean.class, Character.class, Byte.class, Short.class, Integer.class, Long.class, Float.class, Double.class, Void.class));


    public static boolean isPrimitive(Class mClass) {
        return WRAPPER_PRIMITIVES.contains(mClass) || mClass.isPrimitive();
    }

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


    /** Returns all fields of class {@code mClass} including the fields of all its subclasses.
     *
     * @param mClass the class whose fields (including its subclass') will be returned.
     * @return an array containing the fields of {@code mClass} and the fields of all its subclasses.
     */
    public static Field[] getAllFields(Class<?> mClass) {
        Field[] mClassFields = new Field[]{};
        while ( mClass != null && mClass != Object.class){
            mClassFields = Stream.concat(Arrays.stream(mClassFields), Arrays.stream(mClass.getDeclaredFields()))
                    .toArray(Field[]::new);
            mClass = mClass.getSuperclass();
        }
        return mClassFields;
    }

}
