using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ORB.vson.parsers
{
    public static class ObjectParser
    {
        private static Dictionary<int, object> _objectMap;


        public static object FromJson(string text)
        {
            var jsonObject = JObject.Parse(text);

            var targetClass = Type.GetType(jsonObject.GetValue("type").ToString()); // get class name from "type" entry

            if (FieldUtils.IsPrimitive(targetClass))

                return Convert.ChangeType(jsonObject.GetValue("value"), targetClass);


            if (targetClass == typeof(string))

                return Activator.CreateInstance(typeof(string),
                    Convert.ChangeType(jsonObject.GetValue("value"), typeof(string)).ToString().ToCharArray());


            var targetConstructor = targetClass.GetConstructor(Type.EmptyTypes); // get noArgs constructor
            var targetObject = targetConstructor.Invoke(Type.EmptyTypes); // create an instance of targetClass
            var props = JObject.Parse(jsonObject.GetValue("properties").ToString());

            var hash = Convert.ToInt32(props.GetValue("@hash").ToString());

            if (_objectMap.ContainsKey(hash))
            {
                return _objectMap[hash];
            }

            FieldParser.FromJson(targetObject, props); // pass the instance to the field parser
            // where all attribute key-val-pairs will be extracted from the "properties" field of the JSON
            _objectMap[hash] = targetObject;

            return targetObject;
        }

        public static void InitObjectMap()
        {
            _objectMap = new Dictionary<int, object>();
        }

        public static void ClearObjectMap()
        {
            _objectMap = null;
        }
    }
}