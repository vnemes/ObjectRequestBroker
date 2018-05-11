using System.Text;

namespace ORB.vson.writers
{

    public static class ObjectWriter
    {

        public static string ToJson(object @object)
        {
            var sb = new StringBuilder();

            var mClass = @object.GetType();

            sb.Append("{\n").Append("\"").Append("type").Append("\"").Append(": "); // "type":"ClassName"

            sb.Append("\"").Append(mClass.FullName).Append("\"").Append(",\n");

            if (FieldUtils.IsPrimitive(mClass) || mClass == typeof(string))
            {
                sb.Append("\"value\": ").Append(FieldWriter.WriteFieldValue(@object)).Append("\n}");
                return sb.ToString();
            }

            sb.Append("\"").Append("properties").Append("\"").Append(": "); // "properties":{ "attributeName":"attributeValue" }

            sb.Append("{");
            sb.Append("\n\"@hash\": ").Append(@object.GetHashCode()).Append(", ");
            var mClassFields = FieldUtils.GetAllFields(mClass);
            foreach (var f in mClassFields)
            {
                sb.Append("\n");


                sb.Append(FieldWriter.ToJson(f, @object));
                sb.Append(",");
            }
            // remove trailing comma
            sb.Remove(sb.Length - 1, 1).Append("\n}");


            sb.Append("\n}");
            return sb.ToString();
        }

    }

}
