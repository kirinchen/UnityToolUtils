using System;
using System.Reflection;
using UnityEngine;


namespace surfm.tool {

    public class InjectUtils {


        public static void inject(object o) {
            FieldInfo[] fields = o.GetType().GetFields(
                         BindingFlags.NonPublic | BindingFlags.Public |
                         BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++) {
                injectField(o, fields[i]);
            }
        }

        private static void injectField(object obj, FieldInfo f) {
            ValueAttribute va = f.GetCustomAttribute<ValueAttribute>();
            if (va != null) {
                setupValue(va,obj,f);
                return;
            }

            InjectAttribute ia = f.GetCustomAttribute<InjectAttribute>();
            if (ia == null) return;
            object ino = findObj(obj, f, ia);
            f.SetValue(obj, ino);
        }

        private static void setupValue(ValueAttribute va, object obj, FieldInfo f) {
            object val = ConstantRepo.getInstance().get(va.name);
            f.SetValue(obj, val);
        }

        private static object findObj(object o, FieldInfo f, InjectAttribute ia) {
            if (ia.type == InjectAttribute.Type.global) {
                return BeansRepo.bean(f.FieldType, ia.name);
            } else {
                if (!(o is MonoBehaviour)) throw new System.Exception("this not MonoBehaviour=" + o + " f=" + f);
                MonoBehaviour mo = (MonoBehaviour)o;
                if (ia.type == InjectAttribute.Type.getcomponent) return mo.GetComponent(f.FieldType);
                if (ia.type == InjectAttribute.Type.getcomponentinchildren) return mo.GetComponentInChildren(f.FieldType);
                if (ia.type == InjectAttribute.Type.getComponentInParent) return mo.GetComponentInParent(f.FieldType);
                throw new System.Exception("this not Support this type=" + ia.type);
            }
        }



    }
}