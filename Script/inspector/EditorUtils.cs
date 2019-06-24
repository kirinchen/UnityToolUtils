﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace surfm.tool {
    public class EditorUtils {


        public static void popupList(string title, List<string> l, string fieldN, SerializedProperty sp) {
            SerializedProperty fileSP = sp.FindPropertyRelative(fieldN);
            Func<string, string> inout = (s) => {
                if (!string.IsNullOrEmpty(s)) {
                    fileSP.stringValue = s;
                }
                return fileSP.stringValue;
            };
            popupList(title, l, inout);
        }

        public static int popupList(string title, List<string> l, Func<string, string> inout, GUIStyle style = null) {
            int idx = -1;
            string enumName = inout(null);
            if (!string.IsNullOrEmpty(enumName) && l.Contains(enumName)) {
                idx = l.IndexOf(enumName);
            }
            if (style == null) {
                idx = EditorGUILayout.Popup(title, idx, l.ToArray());
            } else {
                idx = EditorGUILayout.Popup(title, idx, l.ToArray(), style);
            }
            if (idx >= 0) {
                inout(l[idx]);
            }
            return idx;

        }


        public static string getParentPath(SerializedProperty prop) {
            int lastDot = prop.propertyPath.LastIndexOf('.');
            if (lastDot == -1) // No parent property
                return "";
            return prop.propertyPath.Substring(0, lastDot);
        }

        public static SerializedProperty getParent(SerializedProperty prop) {
            string prentPath = getParentPath(prop);
            return prop.serializedObject.FindProperty(prentPath);
        }

        public static object draw(string lan, object obj) {
            
            if (obj.GetType() == typeof(int)) {
                return  EditorGUILayout.IntField(lan, (int)obj);
            }
            if (obj.GetType() == typeof(float)) {
                return EditorGUILayout.FloatField(lan, (float)obj);
            }
            if (obj.GetType() == typeof(string)) {
                return EditorGUILayout.TextField(lan, obj.ToString());
            }
            if (obj.GetType() == typeof(bool)) {
                return EditorGUILayout.Toggle(lan, Convert.ToBoolean(obj));
            }
            if (obj.GetType().IsEnum) {
                string[] ns= Enum.GetNames(obj.GetType());
                int idx = ns.ToList().FindIndex(e => e.Equals(obj.ToString()) );
                idx = EditorGUILayout.Popup(lan, idx, ns);
                return Enum.Parse(obj.GetType(),ns[idx]);
            }

            return null;
        }


    }




}
#endif