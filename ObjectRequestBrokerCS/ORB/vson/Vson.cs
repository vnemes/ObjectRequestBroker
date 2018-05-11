using ORB.vson.parsers;
using ORB.vson.writers;

namespace ORB.vson
{


    public static class Vson
    {

        /// <summary>
        /// Serializer.
        /// Saves the state of object {@code object} to a JSON string.
        /// </summary>
        /// <param name="object"> the object whose state will be stored. </param>
        /// <returns> a string containing the JSON representation of {@code object}. </returns>

        public static string ToJson(object @object)
        {
            return ObjectWriter.ToJson(@object);
        }

        /// <summary>
        /// Deserializer.
        /// Restores a previous state of an object from the JSON string {@code string}.
        /// </summary>
        /// <param name="string"> the string representation of the JSON containing the saved state of an object. </param>
        /// <returns> an object whose attributes were stored in the JSON {@code string}. </returns>

        public static object FromJson(string @string)
        {
            ObjectParser.InitObjectMap();
            var parsedObject = ObjectParser.FromJson(@string);
            ObjectParser.ClearObjectMap();
            return parsedObject;
        }

    }
}
