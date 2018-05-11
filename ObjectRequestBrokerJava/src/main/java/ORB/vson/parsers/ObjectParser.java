package ORB.vson.parsers;

import org.json.JSONObject;
import ORB.vson.FieldUtils;

import java.lang.reflect.Constructor;
import java.util.HashMap;

import static ORB.vson.FieldUtils.isPrimitive;

public class ObjectParser {

    private static HashMap<Integer, Object> objectMap = null;

    public ObjectParser() {
    }


    public static Object fromJson(String text) {
        JSONObject jsonObject = new JSONObject(text);
        try {
            Class<?> targetClass = Class.forName(jsonObject.get("type").toString().replace("System.","java.lang.")); // get class name from "type" entry

            if (isPrimitive(targetClass) || targetClass.equals(String.class)){
                Constructor targetConstructor = targetClass.getConstructor(FieldUtils.attemptPrimitiveConvesion(targetClass));
                Object targetObject = targetConstructor.newInstance(jsonObject.get("value"));
                return targetObject;
            }


            Constructor targetConstructor = targetClass.getConstructor(); // get noArgs constructor
            Object targetObject = targetConstructor.newInstance();  // create an instance of targetClass
            JSONObject props = new JSONObject(jsonObject.get("properties").toString());

            int hash = Integer.valueOf(props.get("@hash").toString());
            if (objectMap.containsKey(hash)) {
                return objectMap.get(hash);
            }
            else {
                FieldParser.fromJson(targetObject, props); // pass the instance to the field parser
                // where all attribute key-val-pairs will be extracted from the "properties" field of the JSON
                objectMap.put(hash,targetObject);
            }
            return targetObject;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    public static void initObjectMap() {
        objectMap = new HashMap<>();
    }

    public static void clearObjectMap() {
        objectMap = null;
    }


}
