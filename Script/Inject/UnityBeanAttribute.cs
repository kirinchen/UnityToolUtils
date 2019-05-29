using System;

namespace surfm.tool {
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class UnityBeanAttribute : Attribute {

        public string name = BeanAttribute.DEFAULT;
        public bool cache = true;


    }
}
