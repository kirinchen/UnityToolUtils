using UnityEngine;
namespace surfm.tool {

    public static class VectorUtils {

        public static Vector3 toXZVec3(this Vector2 v, float y = 0) => new Vector3(v.x, y, v.y);
        public static Vector2 toXZVec2(this Vector3 v) => new Vector2(v.x, v.z);

    }
}
