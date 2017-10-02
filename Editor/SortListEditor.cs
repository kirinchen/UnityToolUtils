using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using Rotorz.ReorderableList;

namespace surfm.tool.i18n {
    public abstract class SortListEditor<T> : Editor where T : MonoBehaviour {

        private List<SerializedProperty> _listPropertys = new List<SerializedProperty>();
        protected T db
        {
            get
            {
                return target as T;
            }
        }

        private void OnEnable() {
            foreach (string n in listPropertyNames()) {
                _listPropertys.Add(serializedObject.FindProperty(n));
            }
        }

        public abstract string[] listPropertyNames();

        internal abstract bool isSort();

        internal abstract void setSort(bool b);

        internal virtual void onDrawDefaultInspector() {
            DrawDefaultInspector();
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            if (isSort()) {
                GUIContent cont = new GUIContent("sort", "Check to enable loading of progress made in leveling and perk unlocking from previous save");
                setSort(EditorGUILayout.Toggle(cont, isSort()));
                foreach (SerializedProperty sp in _listPropertys) {
                    ReorderableListGUI.Title(sp.name);
                    ReorderableListGUI.ListField(sp, ReorderableListFlags.ShowIndices);
                }
            } else {
                onDrawDefaultInspector();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}