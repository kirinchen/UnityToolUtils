using UnityEngine;
using System.Collections;
using System;

namespace surfm.tool {
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class ValueAttribute : Attribute {

        public string name;

        public ValueAttribute(string n) {
            name = n;
        }

    }
}
