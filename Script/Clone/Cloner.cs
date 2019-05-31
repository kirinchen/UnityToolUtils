using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace surfm.tool {


    public class Cloner {
        private object src;
        private object tar;

        internal Cloner(object s, object t) {
            src = s;
            tar = t;
        }

        internal Cloner clone() {
            clone(src,tar);
            return this;
        }


        private void clone(object s, object t) {
            List<FieldInfo> sFields = s.GetType().GetFields().ToList();
            List<FieldInfo> tFields = t.GetType().GetFields().ToList();
            foreach (FieldInfo sF in sFields) {
                FieldInfo tField = tFields.Find(tf => { return tf.Name.Equals(sF.Name); });
                if (sF.ReflectedType.Equals(tField.ReflectedType)) {
                    object sv = sF.GetValue(s);
                    tField.SetValue(t, sv);
                } else {
                    object sv = sF.GetValue(s);
                    object tv = tField.GetValue(t);
                    clone(sv,tv);
                }
            }
        }

        public object getTarget() {
            return tar;
        }


    }

}
