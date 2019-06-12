using CodeStage.AntiCheat.ObscuredTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace surfm.tool {
    public class ObscuredValueConverter : JsonConverter {

        public static readonly ObscuredValueConverter DEFAULT = new ObscuredValueConverter();

        public class Conv {

            public Type blunt;
            public Func<object, object> ObscuredToObj;
            public Func<object, object> ObjToObscured;

            internal Conv(Type b, Func<object, object> _ObscuredToObj, Func<object, object> _ObjToObscured) {
                blunt = b;
                ObscuredToObj = _ObscuredToObj;
                ObjToObscured = _ObjToObscured;
            }

        }


        private Dictionary<Type, Conv> list = new Dictionary<Type, Conv>();

        public object toBlunt(object o) {
            if (list.ContainsKey(o.GetType())) {
                return list[o.GetType()].ObscuredToObj(o);
            }
            return o;
        }

        public object toObscured(object o) {
            Conv conv = list.Values.ToList().Find(c => o.GetType() == c.blunt);
            if (conv != null) {
                return list[o.GetType()].ObjToObscured(o);
            }
            return o;
        }

    

        private readonly Type[] _types = {
            typeof(ObscuredInt),
            typeof(ObscuredFloat),
            typeof(ObscuredDouble),
            typeof(ObscuredDecimal),
            typeof(ObscuredChar),
            typeof(ObscuredByte),
            typeof(ObscuredBool),
            typeof(ObscuredLong),
            typeof(ObscuredQuaternion),
            typeof(ObscuredSByte),
            typeof(ObscuredShort),
            typeof(ObscuredUInt),
            typeof(ObscuredULong),
            typeof(ObscuredUShort),
            typeof(ObscuredVector2),
            typeof(ObscuredVector3),
            typeof(ObscuredString)
        };


        public ObscuredValueConverter() {
            list.Add(typeof(ObscuredInt), new Conv(
                 typeof(int),
                 o => { return ((int)(ObscuredInt)o); },
                 o => { ObscuredInt value = Convert.ToInt32(o); return value; }
            ));
            list.Add(typeof(ObscuredFloat), new Conv(
               typeof(float),
                 o => { return ((float)(ObscuredFloat)o); },
               o => { ObscuredFloat value = Convert.ToSingle(o); return value; }
            ));
            list.Add(typeof(ObscuredDouble), new Conv(
                typeof(double),
                o => { return ((double)(ObscuredDouble)o); },
                 o => { ObscuredDouble value = Convert.ToSingle(o); return value; }
            ));

            list.Add(typeof(ObscuredBool), new Conv(
                typeof(bool),
                 o => { return ((bool)(ObscuredBool)o); },
                 o => { ObscuredBool value = Convert.ToBoolean(o); return value; }
            ));

            list.Add(typeof(ObscuredString), new Conv(
                 typeof(string),
                o => { return ((string)(ObscuredString)o); },
                o => { ObscuredString value = o + ""; return value; }
            ));
        }

        #region implemented abstract members of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            Type ty = value.GetType();
            if (list.ContainsKey(ty)) {
                Conv cv = list[ty];
                object val = cv.ObscuredToObj(value);
                writer.WriteValue(val);
            } else {

                throw new Exception("not suppot ty=" + ty);
            }
            //if (value is ObscuredInt) {
            //    writer.WriteValue((int)(ObscuredInt)value);
            //} else if (value is ObscuredBool) {
            //    writer.WriteValue((bool)(ObscuredBool)value);
            //} else if (value is ObscuredFloat) {
            //    writer.WriteValue((float)(ObscuredFloat)value);
            //} else {
            //    Debug.Log("ObscuredValueConverter type " + value.GetType().ToString() + " not implemented");
            //    writer.WriteValue(value.ToString());
            //}
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.Value != null) {

                if (list.ContainsKey(objectType)) {
                    return list[objectType].ObjToObscured(reader.Value);
                } else {
                    throw new Exception("not suppot ty=" + objectType);
                }

            }
            return null;
        }

        public override bool CanConvert(Type objectType) {
            return _types.Any(t => t == objectType);
        }

        #endregion
    }

}


