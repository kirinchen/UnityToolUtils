using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class ConstAttribute : Attribute {
        public string key;

        public ConstAttribute(string k) {
            key = k;
        }
    }
}
