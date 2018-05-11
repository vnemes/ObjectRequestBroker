package ORB.vson.writers;


import java.lang.reflect.Array;
import java.lang.reflect.Field;

import static ORB.vson.FieldUtils.isPrimitive;

public class FieldWriter {

    public FieldWriter() {
    }


    public static String toJson(Field f, Object object) {
        StringBuffer sb = new StringBuffer();

        sb.append("\"").append(f.getName()).append("\"").append(": ");

        try {
            sb.append(writeFieldValue(f.get(object)));
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }

        return sb.toString();
    }


    public static String writeFieldValue(Object object) {
        StringBuilder sb = new StringBuilder();

        if (isPrimitive(object.getClass())) { // attribute is primitive
            sb.append(String.valueOf(object));

        }else if (object.getClass().equals(String.class)) { // attribute is string
            sb.append("\"").append(String.valueOf(object)).append("\"");

        } else if (object.getClass().isArray()) { // attribute is array

            sb.append("[\n");

            for (int i = 0; i < Array.getLength(object); i++)
                sb.append(writeFieldValue(Array.get(object, i))).append(",\n");
            // delete last comma
            sb.replace(sb.length() - 2, sb.length() - 1, "").append("]");

        } else   // attribute is an Object
            sb.append(ObjectWriter.toJson(object));

        return sb.toString();
    }


}
