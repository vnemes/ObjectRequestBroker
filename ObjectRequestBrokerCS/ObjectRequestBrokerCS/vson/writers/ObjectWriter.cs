using System;
using System.Reflection;
using System.Text;

namespace ObjectRequestBrokerCS.vson.writers
{

    public class ObjectWriter
    {

        public ObjectWriter()
        {
        }


        public static string toJson(object @object)
        {
            StringBuilder sb = new StringBuilder();

            Type mClass = @object.GetType();

            sb.Append("{\n").Append("\"").Append("type").Append("\"").Append(": "); // "type":"ClassName"

            sb.Append("\"").Append(mClass.FullName).Append("\"").Append(",\n");

            if (FieldUtils.isPrimitive(mClass) || mClass.Equals(typeof(string)))
            {
                sb.Append("\"value\": ").Append(FieldWriter.writeFieldValue(@object)).Append("\n}");
                return sb.ToString();
            }

            sb.Append("\"").Append("properties").Append("\"").Append(": "); // "properties":{ "attributeName":"attributeValue" }

            sb.Append("{");
            sb.Append("\n\"@hash\": ").Append(@object.GetHashCode()).Append(", ");
            FieldInfo[] mClassFields = FieldUtils.getAllFields(mClass);
            foreach (FieldInfo f in mClassFields)
            {
                sb.Append("\n");


                sb.Append(FieldWriter.toJson(f, @object));
                sb.Append(",");
            }
            // remove trailing comma
            sb.Remove(sb.Length - 1, 1).Append("\n}");


            sb.Append("\n}");
            return sb.ToString();
        }

    }

}
