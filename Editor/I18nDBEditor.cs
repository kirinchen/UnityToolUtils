using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
namespace surfm.tool.i18n {
    [CustomEditor(typeof(I18nDB))]
    public class I18nDBEditor : SortListEditor<I18nDB> {
        private readonly object fileName;

        public override string[] listPropertyNames() {
            return new string[] { "terms" };
        }

        internal override bool isSort() {
            return db.sort;
        }

        internal override void setSort(bool b) {
            db.sort = b;
        }
        private int buffCutdown;


        internal override void onDrawDefaultInspector() {
            string deupicateName = getDeupicateName();
            if (!string.IsNullOrEmpty(deupicateName)) {
                db.search = deupicateName;
                EditorGUILayout.LabelField("Depicate name : " + deupicateName, EditorStyles.boldLabel);
            }

            if (string.IsNullOrEmpty(db.search)) {
    
                if (GUILayout.Button("Export File...")) {
                    exportFile();
                }
                if (GUILayout.Button("Import File...")) {
                    importFile();
                }
                base.onDrawDefaultInspector();
            } else {
                GUIContent cont = new GUIContent("search", "Check to enable loading of progress made in leveling and perk unlocking from previous save");
                db.search = (EditorGUILayout.TextField(cont, db.search));
                serializedObject.Update();
                List<I18nDB.Term> lits = new List<I18nDB.Term>();
                db.terms.ForEach(i => { lits.Add(i); });
                ShowList(db.search, serializedObject.FindProperty("terms"), lits);
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void importFile() {
            string fileName = "exportI18n.csv";
            string text = File.ReadAllText(fileName);
            CvsList cl = new CvsList(text);
            List<I18nDB.Term> ts = cl.convert(() => { return new I18nDB.Term(); }, getRowValue);
            db.terms = ts;
            Debug.Log("import count=" + ts.Count);
        }

        private string getRowValue(CvsList.Row r, string k) {
            string ans = r.getValue(k);
            if (r.getValue("Escapeble").Equals("1")) {
                ans = repaceEscape(ans);
            }
            return ans;
        }

        private static string repaceEscape(string ans) {
            ans = ans.Trim();
            return ans.Replace("&para;", "\n").Replace("&cedil;", ",");
        }

        private void exportFile() {
            string fileName = "exportI18n.csv";
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists) {
                fi.Delete();
            }
            CvsList cl = new CvsList(db.getSchema(), db.listRows());
            var sr = File.CreateText(fileName);
            cl.output(s => {
                sr.WriteLine(s);
            });
            sr.Close();
            fi.Refresh();
            Debug.Log("export " + fi.FullName + " Exists=" + fi.Exists);
        }




        private void addNew(string key, string prefix = null) {
            I18nDB.Term term = db.terms.Find(t => { return string.Equals(t.name, key); });
            if (term == null) {
                I18nDB.Term n = new I18nDB.Term();
                n.name = key;
                n.eng = n.cn = n.es = n.kr = n.pt = n.ru = n.tw = "TODO " + key + prefix;
                db.terms.Add(n);
            }
        }

        private string getDeupicateName() {
            List<string> check = new List<string>();
            foreach (I18nDB.Term lt in db.terms) {
                if (check.Contains(lt.name)) {
                    return lt.name;
                } else {
                    check.Add(lt.name);
                }
            }
            return string.Empty;
        }


        public void ShowList(string qs, SerializedProperty list, List<I18nDB.Term> its) {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
            if (list.isExpanded) {
                for (int i = 0; i < list.arraySize; i++) {
                    SerializedProperty sp = list.GetArrayElementAtIndex(i);
                    I18nDB.Term item = its[i];
                    if (contains(item, qs)) {
                        string temp = "{0} ({1})";
                        EditorGUILayout.PropertyField(sp, new GUIContent(string.Format(temp, i, item.name)), true);
                    }

                }
            }
            EditorGUI.indentLevel -= 1;
        }

        private bool contains(I18nDB.Term t, string qs) {
            foreach (I18nDB.Language l in Enum.GetValues(typeof(I18nDB.Language))) {
                string v = t.get(l);
                v = v.ToLower();
                qs = qs.ToLower();
                Match match = Regex.Match(v, qs);
                if (match.Success) {
                    return true;
                }
            }
            return false;
        }
    }
}
