using System;

namespace surfm.tool {
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class BeanAttribute : Attribute {

        public  const string DEFAULT = "@DEFAULT@";

        public string name = DEFAULT;
        public bool instanced = false;

    }
}
