using UnityEngine;
namespace surfm.tool {

    public static class VectorUtils {

        public static Vector2 toXZ(this Vector3 v) {
            return new Vector3(v.x, v.z);
        }

    }
}
