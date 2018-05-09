using System;
using System.Collections.Generic;
using ObjectRequestBrokerCS.vson;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace ObjectRequestBrokerCS.vson.parsers
{

    public class ObjectParser
    {

        private static Dictionary<int, object> objectMap = null;

        public ObjectParser()
        {
        }


        public static object fromJson(string text)
        {
            JObject jsonObject = JObject.Parse(text);
            try
            {
                Type targetClass = Type.GetType(jsonObject.GetValue("type").ToString()); // get class name from "type" entry

                if (FieldUtils.isPrimitive(targetClass) || targetClass.Equals(typeof(string)))
                {
                    //ConstructorInfo targetConstructor = targetClass.GetConstructor(new[] { FieldUtils.attemptPrimitiveConvesion(targetClass) });
                    return targetClass.GetConstructor(new[] { FieldUtils.attemptPrimitiveConvesion(targetClass) }).Invoke(new[] { jsonObject.GetValue("value") });
                }


                ConstructorInfo targetConstructor = targetClass.GetConstructor(Type.EmptyTypes); // get noArgs constructor
                object targetObject = targetConstructor.Invoke(Type.EmptyTypes); // create an instance of targetClass
                JObject props = JObject.Parse(jsonObject.GetValue("properties").ToString());

                int hash = Convert.ToInt32(props.GetValue("@hash").ToString());

                if (objectMap.ContainsKey(hash))
                {
                    return objectMap[hash];
                }
                else
                {
                    FieldParser.fromJson(targetObject, props); // pass the instance to the field parser
                                                               // where all attribute key-val-pairs will be extracted from the "properties" field of the JSON
                    objectMap[hash] = targetObject;
                }
                return targetObject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return null;
            }
        }

        public static void initObjectMap()
        {
            objectMap = new Dictionary<int, object>();
        }

        public static void clearObjectMap()
        {
            objectMap = null;
        }


    }
}

