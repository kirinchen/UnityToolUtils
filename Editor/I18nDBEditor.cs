using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace surfm.tool.i18n {
    [CustomEditor(typeof(I18nDB))]
    public class I18nDBEditor : Editor {
        private readonly object fileName;
        protected I18nDB db { get { return target as I18nDB; } }

        private string _addName;
        public override void OnInspectorGUI() {
            serializedObject.Update();

            if (GUILayout.Button("SAVE")) {
                EditorUtility.SetDirty(target);
            }

            db.recordLostKeys = EditorGUILayout.Toggle("recordLostKeys", db.recordLostKeys);

            List<SystemLanguage> sls = EnumHelper.ToList<SystemLanguage>();
            int idx = EditorUtils.popupList("Default Lang", sls.ConvertAll(s => s.ToString()), s => db.defaultLanguage.ToString());
            if (idx >= 0) db.defaultLanguage = sls[idx];

            EditorGUILayout.BeginHorizontal("box");
            _addName = GUILayout.TextField(_addName);
            if (GUILayout.Button("ADD")) {
                db.terms.Add(new I18nDB.Term(_addName));
                _addName = "";
            }
            EditorGUILayout.EndHorizontal();

            db.terms.ForEach(showTerm);
            //EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
        }

        private void showTerm(I18nDB.Term obj) {
            EditorGUILayout.BeginVertical("box");
            obj.name = GUILayout.TextField(obj.name);
            selectLang(obj);
            new List<I18nDB.TermLang>(obj.termLangs).ForEach(tl => showLangTerm(obj, tl));
            EditorGUILayout.EndVertical();
        }

        private void showLangTerm(I18nDB.Term rootObj, I18nDB.TermLang obj) {
            EditorGUILayout.BeginHorizontal("box");
            obj.value = EditorGUILayout.TextField(obj.language.ToString(), obj.value);
            if (GUILayout.Button("DEL")) {
                rootObj.termLangs.Remove(obj);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void selectLang(I18nDB.Term obj) {
            List<SystemLanguage> sls = EnumHelper.ToList<SystemLanguage>();
            sls = sls.FindAll(s => !obj.termLangs.Exists(tl => tl.language == s));
            int idx = EditorUtils.popupList("Add Lang", sls.ConvertAll(s => s.ToString()), s => s);
            if (idx >= 0) {
                obj.termLangs.Add(new I18nDB.TermLang(sls[idx]));
            }
        }
    }
}
