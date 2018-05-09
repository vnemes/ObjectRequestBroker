using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRequestBrokerCS.vson.parsers
{
    using ObjectRequestBrokerCS.vson;
    using System.Reflection;
    using Newtonsoft.Json.Linq;


    class FieldParser
    {
        public FieldParser()
        {
        }


        internal static void fromJson(object @object, JObject targetAttributes)
        {

            FieldInfo[] mClassFields = FieldUtils.getAllFields(@object.GetType()); //get fields in this class

            foreach (FieldInfo f in mClassFields)
            {
                /*
            if (!f.IsPrivate)
            { // set private fields accessible in order to obtain their value
                f.IsPrivate = true;
                parseFieldValue(@object, targetAttributes, f);
                f.Accessible = false;
            }
            else
            {
                parseFieldValue(@object, targetAttributes, f);
            }
            */
                parseFieldValue(@object, targetAttributes, f);
            }

        }

        private static void parseFieldValue(object @object, JObject targetAttributes, FieldInfo f)
        {
            try
            {
                Type t = f.FieldType;
                if (FieldUtils.isPrimitive(f.FieldType) || f.FieldType.Equals(typeof(string)))
                { // attribute is primitive
                    f.SetValue(@object, Convert.ChangeType(targetAttributes.GetValue(f.Name), t));
                }
                else if (f.FieldType.IsArray)
                { // attribute is array //
                    JArray jsonArr = (JArray)targetAttributes.GetValue(f.Name);
                    f.SetValue(@object, Array.CreateInstance(f.FieldType.GetElementType(), jsonArr.Count)); // invoke array constructor
                    if (FieldUtils.isPrimitive(f.FieldType.GetElementType()) || f.FieldType.GetElementType().Equals(typeof(string)) || f.FieldType.GetElementType().Equals(typeof(object)))
                    {
                        for (int i = 0; i < jsonArr.Count; i++) // array element type is primitive
                        {
                            ((Array)f.GetValue(@object)).SetValue(jsonArr[i], i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < jsonArr.Count; i++) // array element type is object
                        {
                            ((Array)f.GetValue(@object)).SetValue(ObjectParser.fromJson(jsonArr[i].ToString()), i);
                        }
                    }
                }
                else // attribute is an Object
                {
                    f.SetValue(@object, ObjectParser.fromJson(targetAttributes.GetValue(f.Name).ToString()));
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
