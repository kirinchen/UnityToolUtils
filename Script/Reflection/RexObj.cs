using System.Collections.Generic;
using System.Reflection;
namespace surfm.tool {
    public class RexObj<T> {

        private List<AsField<T>> fields ;
        public T obj { get; private set; }

        public RexObj(T t) {
            obj = t;
            fields = ReflectionTool.listFields<T>().ConvertAll(f => new AsField<T>(this, f));
        }

        public List<AsField<T>> listFields() {
            return fields;
        }

        public AsField<T> getField(FieldInfo info) {
            return fields.Find(f=> f.field.Equals(info) );
        }

        public void setVal(AsField<T> field,object val) {
            ReflectionTool.setVal(field.field,obj,val);
        }

        public object getVal(AsField<T> field) {
            return ReflectionTool.getVal(field.field,obj);
        }




    }

    public class AsField<FT> {

        public RexObj<FT> obj { get; private set; }
        public FieldInfo field { get; private set; }
        internal AsField(RexObj<FT> o, FieldInfo fi) {
            obj = o;
            field = fi;
        }

        public void setVal(object o) {
            obj.setVal(this, o);
        }

        public object getVal() {
            return obj.getVal(this);
        }


    }
}