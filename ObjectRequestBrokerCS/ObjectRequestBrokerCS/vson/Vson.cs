using ObjectRequestBrokerCS.vson.parsers;
using ObjectRequestBrokerCS.vson.writers;

namespace ObjectRequestBrokerCS.vson
{


    public class Vson
    {

        /// <summary>
        /// Serializer.
        /// Saves the state of object {@code object} to a JSON string.
        /// </summary>
        /// <param name="object"> the object whose state will be stored. </param>
        /// <returns> a string containing the JSON representation of {@code object}. </returns>

        public static string toJson(object @object)
        {
            string jsonRepresentation = ObjectWriter.toJson(@object);
            return jsonRepresentation;
        }

        /// <summary>
        /// Deserializer.
        /// Restores a previous state of an object from the JSON string {@code string}.
        /// </summary>
        /// <param name="string"> the string representation of the JSON containing the saved state of an object. </param>
        /// <returns> an object whose attributes were stored in the JSON {@code string}. </returns>

        public static object fromJson(string @string)
        {
            ObjectParser.initObjectMap();
            object parsedObject = ObjectParser.fromJson(@string);
            ObjectParser.clearObjectMap();
            return parsedObject;
        }

    }
}
