using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace surfm.tool {

    public class ReflectionTool  {

        public static List<FieldInfo> listFields<T>() {
            Type t= typeof(T);
            return t.GetFields().ToList();
        }

        public static void setVal( FieldInfo fi, object o , object v) {
            fi.SetValue(o,v);
        }

        public static object getVal(FieldInfo fi, object o) {
            return fi.GetValue(o);
        }

        public static RexObj<O> newObj<O>(O o) {
            return new RexObj<O>(o);
        }



    }
}