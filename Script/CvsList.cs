using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
namespace surfm.tool {
    public class CvsList {

        private string content;
        private Dictionary<string, int> schema = new Dictionary<string, int>();
        private List<Row> rowData = new List<Row>();

        public CvsList(string content) {
            this.content = content;
            parse();
        }

        public CvsList(Dictionary<string, int> schema, List<string[]> rows) {
            this.schema = schema;
            rowData = rows.ConvertAll(r => { return new Row(this, r); });
        }

        private void parse() {
            foreach (string s in content.Split('\r', '\n')) {
                if (!parseSchema(s)) {
                    string row = s.Trim();
                    if (row.Length > 0) {
                        Row el = parse(row.Split(','));
                        rowData.Add(el);
                    }
                }
            }
        }

        public List<T> convert<T>(Func<T> newF, Func<Row, string, string> CGetV = null) {
            Func<Row, string, string> getV = CGetV == null ? getRowValue : CGetV;
            List<T> ans = new List<T>();
            foreach (Row r in rowData) {
                T t = newF();
                inject(r, t, getV);
                ans.Add(t);
            }
            return ans;
        }

        private string getRowValue(Row r, string key) {
            return r.getValue(key);
        }

        private void inject<T>(Row r, T t, Func<Row, string, string> getV) {
            FieldInfo[] fs = t.GetType().GetFields();
            foreach (FieldInfo f in fs) {
                string d = getV(r, f.Name);
                f.SetValue(t, d);
            }
        }

        public void output(Action<string> lineCB) {
            Action<string[]> la = (ss) => {
                string l = "";
                foreach (string s in ss) {
                    l += s + ",";
                }
                lineCB(l);
            };
            la(getHeader());
            foreach (Row r in rowData) {
                la(r.list);
            }
        }

        private string[] getHeader() {
            string[] ans = new string[schema.Count];
            foreach (string k in schema.Keys) {
                ans[schema[k]] = k;
            }
            return ans;
        }

        private Row parse(string[] v) {
            return new Row(this, v);
        }

        public List<Row> listRows() {
            return rowData;
        }

        private bool parseSchema(string s) {
            if (schema.Count <= 0) {
                string[] ss = s.Split(',');
                for (int i = 0; i < ss.Length; i++) {
                    if (!string.IsNullOrEmpty(ss[i])) {
                        try {
                            schema.Add(ss[i], i);
                        } catch (Exception e) {
                            throw new Exception("ss[i]=" + ss[i] + " i=" + i + " s=" + s, e);
                        }
                    }
                }
                return true;
            } else {
                return false;
            }
        }

        public class Row {
            private CvsList cvs;
            public string[] list { get; private set; }
            internal Row(CvsList c, string[] d) {
                cvs = c;
                list = d;
            }

            public string getValue(int idx) {
                return list[idx];
            }

            public string getValue(string key) {
                try {
                    return list[cvs.schema[key]];
                } catch (Exception e) {
                    string l = "";
                    foreach (string s in list) {
                        l += s + ",";
                    }
                    throw new NullReferenceException("key=" + key + " list=" + l);
                }
            }

        }

    }
}
