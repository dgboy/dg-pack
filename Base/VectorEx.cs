using UnityEngine;

namespace DG_Pack.Base {
    public static class VectorEx {
        public static Vector2 Abs(this Vector2 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y));
        public static Vector2 Swap(this Vector2 v) => new(v.y, v.x);
        public static Vector2 Fixed(this Vector2 v) => new(Mathf.Abs(v.x) >= Mathf.Abs(v.y) ? v.x : 0, Mathf.Abs(v.y) > Mathf.Abs(v.x) ? v.y : 0);
        public static Vector2 Sign(this Vector2 v) =>
            new(v.x == 0 ? v.x : Mathf.Sign(v.x), v.y == 0 ? v.y : Mathf.Sign(v.y));

        public static Quaternion ToFreeRotation(this Vector2 direction) {
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, rotation);
        }
        public static Quaternion ToRotation(this Vector2 direction, float angle = 90) {
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, Mathf.RoundToInt(rotation / angle) * angle);
        }

        public static Vector2 ToDirection(this Quaternion quaternion) => quaternion.eulerAngles;
    }
}