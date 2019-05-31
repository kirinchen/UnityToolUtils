using UnityEngine;
using UnityEditor;


namespace surfm.tool {
    public class CloneUtils  {

        public static T clone<T>(object src, T tar) {
            Cloner cloner = new Cloner(src,tar);
            return (T)cloner.clone().getTarget();
        }

    }
}
