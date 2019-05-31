using System;

namespace surfm.tool {
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class InjectAttribute : Attribute {
        public enum Type {
            global, getcomponent, getcomponentinchildren, getComponentInParent,
        }
        public string name = BeanAttribute.DEFAULT;
        public Type type = Type.global;
    }
}