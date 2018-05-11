package ORB.vson.parsers;

import org.json.JSONArray;
import org.json.JSONObject;

import java.lang.reflect.Array;
import java.lang.reflect.Field;

import static ORB.vson.FieldUtils.getAllFields;
import static ORB.vson.FieldUtils.isPrimitive;

public class FieldParser {

    public FieldParser() {
    }


    static void fromJson(Object object, JSONObject targetAttributes) {

        Field[] mClassFields = getAllFields(object.getClass()); //get fields in this class

        for (Field f : mClassFields) {
            if (!f.isAccessible()) { // set private fields accessible in order to obtain their value
                f.setAccessible(true);
                parseFieldValue(object, targetAttributes, f);
                f.setAccessible(false);
            } else
                parseFieldValue(object, targetAttributes, f);
        }

    }

    private static void parseFieldValue(Object object, JSONObject targetAttributes, Field f) {
        try {
            if (isPrimitive(f.getType()) || f.getType().equals(String.class)) { // attribute is primitive
                f.set(object, targetAttributes.get(f.getName()));
            } else if (f.getType().isArray()) { // attribute is array //
                JSONArray jsonArr = targetAttributes.getJSONArray(f.getName());
                f.set(object, Array.newInstance(f.getType().getComponentType(), jsonArr.length())); // invoke array constructor
                if (isPrimitive(f.getType().getComponentType()) || f.getType().getComponentType().equals(String.class) || f.getType().getComponentType().equals(Object.class)) {
                    for (int i = 0; i < jsonArr.length(); i++) // array element type is primitive
                        Array.set(f.get(object), i, targetAttributes.getJSONArray(f.getName()).get(i));
                } else {
                    for (int i = 0; i < jsonArr.length(); i++) // array element type is object
                        Array.set(f.get(object), i, ObjectParser.fromJson(targetAttributes.getJSONArray(f.getName()).get(i).toString()));
                }
            } else   // attribute is an Object
                f.set(object, ObjectParser.fromJson(targetAttributes.get(f.getName()).toString()));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

}
