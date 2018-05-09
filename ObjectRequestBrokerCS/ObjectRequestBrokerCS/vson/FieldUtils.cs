using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectRequestBrokerCS.vson
{
    public class FieldUtils
    {

        private static readonly ISet<Type> WRAPPER_PRIMITIVES = new HashSet<Type> { typeof(Boolean), typeof(Char), typeof(Byte), typeof(Int16), typeof(Int32), typeof(Int64), typeof(Single), typeof(Double), typeof(void) };


        public static bool isPrimitive(Type mClass)
        {
            return WRAPPER_PRIMITIVES.Contains(mClass) || mClass.IsPrimitive;
        }

        private static IDictionary<Type, Type> builtInMap = new Dictionary<Type, Type>();

        static FieldUtils()
        {
            builtInMap[typeof(Int32)] = typeof(int);
            builtInMap[typeof(Int64)] = typeof(long);
            builtInMap[typeof(Double)] = typeof(double);
            builtInMap[typeof(Single)] = typeof(float);
            builtInMap[typeof(Boolean)] = typeof(bool);
            builtInMap[typeof(Char)] = typeof(char);
            builtInMap[typeof(Byte)] = typeof(byte);
            builtInMap[typeof(void)] = typeof(void);
            builtInMap[typeof(Int16)] = typeof(short);
        }

        public static Type attemptPrimitiveConvesion(Type mClass)
        {
            Type retVal;
            if (!builtInMap.TryGetValue(mClass, out retVal))
            {
                // the key isn't in the dictionary.
                return mClass;
            }
            return retVal;
        }


        /// <summary>
        /// Returns all fields of class {@code mClass} including the fields of all its subclasses.
        /// </summary>
        /// <param name="mClass"> the class whose fields (including its subclass') will be returned. </param>
        /// <returns> an array containing the fields of {@code mClass} and the fields of all its subclasses. </returns>
        public static FieldInfo[] getAllFields(Type mClass)
        {
            FieldInfo[] mClassFields = new FieldInfo[] { };
            while (mClass != null && mClass != typeof(object))
            {
                //Todo check here if more flags are needed
                mClassFields = mClassFields.Concat(mClass.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)).ToArray();
                mClass = mClass.BaseType;
            }
            return mClassFields;
        }

    }

}
