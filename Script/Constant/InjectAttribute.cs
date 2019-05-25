using System;

namespace surfm.tool {
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class InjectAttribute : Attribute {
        public string name = BeanAttribute.DEFAULT;
    }
}