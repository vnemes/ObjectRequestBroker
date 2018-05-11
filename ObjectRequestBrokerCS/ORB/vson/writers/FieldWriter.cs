using System;
using System.Reflection;
using System.Text;

namespace ORB.vson.writers
{

    public static class FieldWriter
    {

        public static string ToJson(FieldInfo f, object @object)
        {
            var sb = new StringBuilder();

            sb.Append("\"").Append(f.Name).Append("\"").Append(": ");

            try
            {
                sb.Append(WriteFieldValue(f.GetValue(@object)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            return sb.ToString();
        }


        public static string WriteFieldValue(object @object)
        {
            var sb = new StringBuilder();

            if (FieldUtils.IsPrimitive(@object.GetType()))
            { // attribute is primitive
                sb.Append(@object.ToString().ToLower());
                // using ToLower in order to transform boolean values "True" and "False" into 
                // the lower case "true" and "false" expected by JObject.Parse()
            }
            else if (@object is string)
            { // attribute is string
                sb.Append("\"").Append(@object).Append("\"");

            }
            else if (@object.GetType().IsArray)
            { // attribute is array

                sb.Append("[\n");

                for (var i = 0; i < ((object[])@object).Length; i++)
                {
                    sb.Append(WriteFieldValue(((object[])@object)[i])).Append(",\n");
                }
                // delete last comma
                sb.Remove(sb.Length - 1, 1).Append("]");

            }
            else // attribute is an Object
            {
                sb.Append(ObjectWriter.ToJson(@object));
            }

            return sb.ToString();
        }


    }
}
