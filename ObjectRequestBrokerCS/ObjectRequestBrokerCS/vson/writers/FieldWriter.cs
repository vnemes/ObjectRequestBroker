using ObjectRequestBrokerCS.vson;
using System;
using System.Reflection;
using System.Text;

namespace ObjectRequestBrokerCS.vson.writers
{


    //JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
    //	import static vson.FieldUtils.isPrimitive;

    public static class FieldWriter
    {

        public static string toJson(FieldInfo f, object @object)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\"").Append(f.Name).Append("\"").Append(": ");

            try
            {
                sb.Append(writeFieldValue(f.GetValue(@object)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            return sb.ToString();
        }


        public static string writeFieldValue(object @object)
        {
            StringBuilder sb = new StringBuilder();

            if (FieldUtils.isPrimitive(@object.GetType()))
            { // attribute is primitive
                sb.Append(@object.ToString().ToLower());
                // using ToLower in order to transform boolean values "True" and "False" into 
                // the lower case "true" and "false" expected by JObject.Parse()
            }
            else if (@object is string)
            { // attribute is string
                sb.Append("\"").Append(@object.ToString()).Append("\"");

            }
            else if (@object.GetType().IsArray)
            { // attribute is array

                sb.Append("[\n");

                for (var i = 0; i < ((object[])@object).Length; i++)
                {
                    sb.Append(writeFieldValue(((object[])@object)[i])).Append(",\n");
                }
                // delete last comma
                sb.Remove(sb.Length - 1, 1).Append("]");

            }
            else // attribute is an Object
            {
                sb.Append(ObjectWriter.toJson(@object));
            }

            return sb.ToString();
        }


    }
}
