package ORB.vson;

import ORB.vson.parsers.ObjectParser;
import ORB.vson.writers.ObjectWriter;

public class Vson {

    /**
     * Serializer.
     * Saves the state of object {@code object} to a JSON string.
     *
     * @param object the object whose state will be stored.
     * @return a string containing the JSON representation of {@code object}.
     */

    public static String toJson(Object object) {
        String jsonRepresentation = ObjectWriter.toJson(object);
        return jsonRepresentation;
    }

    /**
     * Deserializer.
     * Restores a previous state of an object from the JSON string {@code string}.
     *
     * @param string the string representation of the JSON containing the saved state of an object.
     * @return an object whose attributes were stored in the JSON {@code string}.
     */

    public static Object fromJson(String string) {
        ObjectParser.initObjectMap();
        Object parsedObject = ObjectParser.fromJson(string);
        ObjectParser.clearObjectMap();
        return parsedObject;
    }

}
