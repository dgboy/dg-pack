using UnityEngine;

namespace DG_Pack.Base {
    public static class VectorEx {
        public static Vector3 To3(this Vector2 v, float z) => new(v.x, v.y, z);
        public static Vector3 To3(this Vector3 v, float z) => new(v.x, v.y, z);

        public static float Axis(this Vector3 v, Vector2 dir) => dir.x != 0 ? v.x : v.y;
        public static bool OnLine(this Vector2 v, Vector2 target) => 
            Mathf.RoundToInt(v.x) == Mathf.RoundToInt(target.x) || 
            Mathf.RoundToInt(v.y) == Mathf.RoundToInt(target.y);

        public static Vector2 Abs(this Vector2 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y));
        public static Vector2 Swap(this Vector2 v) => new(v.y, v.x);
        public static Vector2 Fixed(this Vector2 v) => new(
            v.x != 0 && Mathf.Abs(v.x) >= Mathf.Abs(v.y) ? Mathf.Sign(v.x) : 0,
            v.x == 0 && Mathf.Abs(v.y) > Mathf.Abs(v.x) ? Mathf.Sign(v.y) : 0
        );
        
        public static Vector2Int FixedInt(this Vector2 v) => new(
            v.x != 0 && Mathf.Abs(v.x) >= Mathf.Abs(v.y) ? Mathf.RoundToInt(Mathf.Sign(v.x)) : 0,
            v.x == 0 && Mathf.Abs(v.y) > Mathf.Abs(v.x) ? Mathf.RoundToInt((int)Mathf.Sign(v.y)) : 0
        );
        public static Vector2 Sign(this Vector2 v) =>
            new(v.x == 0 ? v.x : Mathf.Sign(v.x), v.y == 0 ? v.y : Mathf.Sign(v.y));


        public static float ToAngle(this Vector2 direction) => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        public static Quaternion ToRotation(this Vector2 direction, float start = 90) {
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, rotation - start);
        }
        public static Quaternion ToFixedRotation(this Vector2 direction, float start = 90, float step = 90) {
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, Mathf.RoundToInt(rotation / step) * step - start);
        }


        public static Vector2 ToDirection(this float angle) => new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).Sign();
        public static Vector2 ToDirection(this Quaternion quaternion) => quaternion.eulerAngles;
        public static Vector2 GetRandomDirection(Vector2 prevDir) {
            int angle = Random.Range(-1, 2) * 90;
            return (angle - prevDir.ToAngle()).ToDirection();
        }
    }
}