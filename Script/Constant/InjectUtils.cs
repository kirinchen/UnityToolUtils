using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace surfm.tool {

    public class InjectUtils  {


        public static void inject(object o) {
            FieldInfo[]  fields = o.GetType().GetFields(
                         BindingFlags.NonPublic |
                         BindingFlags.Instance);
            for (int i=0;i<fields.Length;i++) {
                injectField(o,fields[i]);
            }
        }

        private static  void injectField(object obj, FieldInfo f) {
            InjectAttribute ia = f.GetCustomAttribute<InjectAttribute>();
            if (ia == null) return;
            object ino =BeansRepo.bean(f.FieldType,ia.name);
            f.SetValue(obj,ino);
        }



    }
}