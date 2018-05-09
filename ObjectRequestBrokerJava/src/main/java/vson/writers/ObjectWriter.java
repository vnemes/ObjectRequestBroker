package vson.writers;


import java.lang.reflect.Field;

import static vson.FieldUtils.getAllFields;
import static vson.FieldUtils.isPrimitive;

public class ObjectWriter {

    public ObjectWriter() {
    }


    public static String toJson(Object object) {
        StringBuilder sb = new StringBuilder();

        Class<?> mClass = object.getClass();

        sb.append("{\n").append("\"").append("type").append("\"").append(": "); // "type":"ClassName"
        sb.append("\"").append(mClass.getName()).append("\"").append(",\n");

        if (isPrimitive(mClass) || mClass.equals(String.class)){
            sb.append("\"value\": ").append(FieldWriter.writeFieldValue(object)).append("\n}");
            return sb.toString();
        }

        sb.append("\"").append("properties").append("\"").append(": "); // "properties":{ "attributeName":"attributeValue" }

        sb.append("{");
        sb.append("\n\"@hash\": ").append(object.hashCode()).append(", ");
        Field[] mClassFields = getAllFields(mClass);
        for (Field f : mClassFields) {
            sb.append("\n");
            if (!f.isAccessible()) { // set private fields accessible in order to obtain their value
                f.setAccessible(true);
                sb.append(FieldWriter.toJson(f, object));
                f.setAccessible(false);
            } else {
                sb.append(FieldWriter.toJson(f, object));
            }
            sb.append(",");
        }
        // remove trailing comma
        sb.replace(sb.length() - 1, sb.length(), "").append("\n}");


        sb.append("\n}");
        return sb.toString();
    }

}

