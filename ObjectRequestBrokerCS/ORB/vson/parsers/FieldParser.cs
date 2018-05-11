using System;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace ORB.vson.parsers
{
    static class FieldParser
    {

        internal static void FromJson(object @object, JObject targetAttributes)
        {

            FieldInfo[] mClassFields = FieldUtils.GetAllFields(@object.GetType()); //get fields in this class

            foreach (FieldInfo f in mClassFields)
            {

                ParseFieldValue(@object, targetAttributes, f);
            }

        }

        private static void ParseFieldValue(object @object, JObject targetAttributes, FieldInfo f)
        {
            try
            {
                Type mFieldType = f.FieldType;
                if (FieldUtils.IsPrimitive(mFieldType) || mFieldType.Equals(typeof(string)))
                { // attribute is primitive
                    f.SetValue(@object, Convert.ChangeType(targetAttributes.GetValue(f.Name), mFieldType));
                }
                else if (mFieldType.IsArray)
                { // attribute is array //
                    JArray jsonArr = (JArray)targetAttributes.GetValue(f.Name);
                    Type arrayElemType = mFieldType.GetElementType();
                    f.SetValue(@object, Array.CreateInstance(arrayElemType, jsonArr.Count)); // invoke array constructor
                    if (FieldUtils.IsPrimitive(arrayElemType) || arrayElemType == typeof(string) || arrayElemType == typeof(object))
                    {
                        for (int i = 0; i < jsonArr.Count; i++) // array element type is primitive
                        {
                            ((Array)f.GetValue(@object)).SetValue(Convert.ChangeType(jsonArr[i],((JValue)jsonArr[i]).Value.GetType()), i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < jsonArr.Count; i++) // array element type is object
                        {
                            ((Array)f.GetValue(@object)).SetValue(ObjectParser.FromJson(jsonArr[i].ToString()), i);
                        }
                    }
                }
                else // attribute is an Object
                {
                    f.SetValue(@object, ObjectParser.FromJson(targetAttributes.GetValue(f.Name).ToString()));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

    }

}
