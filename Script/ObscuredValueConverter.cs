using CodeStage.AntiCheat.ObscuredTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace surfm.tool {
    public class ObscuredValueConverter : JsonConverter {

        public struct Conv {
            //public Type objType;
            //public Type obscuredType;
            public Func<object, object> ObscuredToObj;
            public Func<object, object> ObjToObscured;
        }


        private Dictionary<Type, Conv> list = new Dictionary<Type, Conv>();

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
            list.Add(typeof(ObscuredInt), new Conv() {
                ObscuredToObj = o => { return ((int)(ObscuredInt)o); },
                ObjToObscured = o => { ObscuredInt value = Convert.ToInt32(o); return value; }
            });
            list.Add(typeof(ObscuredFloat), new Conv() {
                ObscuredToObj = o => { return ((float)(ObscuredFloat)o); },
                ObjToObscured = o => { ObscuredFloat value = Convert.ToSingle(o); return value; }
            });
            list.Add(typeof(ObscuredDouble), new Conv() {
                ObscuredToObj = o => { return ((double)(ObscuredDouble)o); },
                ObjToObscured = o => { ObscuredDouble value = Convert.ToSingle(o); return value; }
            });

            list.Add(typeof(ObscuredBool), new Conv() {
                ObscuredToObj = o => { return ((bool)(ObscuredBool)o); },
                ObjToObscured = o => { ObscuredBool value = Convert.ToBoolean(o); return value; }
            });

            list.Add(typeof(ObscuredString), new Conv() {
                ObscuredToObj = o => { return ((string)(ObscuredString)o); },
               ObjToObscured  = o => { ObscuredString value = o+""; return value; }
            });
        }

        #region implemented abstract members of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            Type ty = value.GetType();
            if (list.ContainsKey(ty)) {
                Conv cv = list[ty];
                object val = cv.ObscuredToObj(value);
                writer.WriteValue(val);
            } else {

                throw new Exception("not suppot ty="+ ty);
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


