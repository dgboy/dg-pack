using System;
using UnityEngine;

namespace DG_Pack.Prototype {
    public enum Direction4 {
        Down = 0,
        Left = 1,
        Right = 2,
        Up = 3,
    }

    public static class Direction4Ex {
        public static Vector2 ToVector2(this Direction4 dir) => dir switch {
            Direction4.Down => Vector2.down,
            Direction4.Left => Vector2.left,
            Direction4.Right => Vector2.right,
            Direction4.Up => Vector2.up,
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null),
        };
        public static Vector2Int ToVector2Int(this Direction4 dir) => dir switch {
            Direction4.Down => Vector2Int.down,
            Direction4.Left => Vector2Int.left,
            Direction4.Right => Vector2Int.right,
            Direction4.Up => Vector2Int.up,
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null),
        };
        public static Vector3 ToVector3(this Direction4 dir) => dir switch {
            Direction4.Down => Vector3.down,
            Direction4.Left => Vector3.left,
            Direction4.Right => Vector3.right,
            Direction4.Up => Vector3.up,
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null),
        };

        public static Quaternion ToRotation(this Direction4 dir) => dir switch {
            Direction4.Down => Quaternion.Euler(0f, 0f, 0f),
            Direction4.Left => Quaternion.Euler(0f, 0f, 0f),
            Direction4.Right => Quaternion.Euler(0f, 0f, 0f),
            Direction4.Up => Quaternion.Euler(0f, 0f, 0f),
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null),
        };

        public static Direction4 From(this Vector2 dir) =>
            Mathf.Abs(dir.x) > Mathf.Abs(dir.y)
                ? Math.Sign(dir.x) == -1 ? Direction4.Left : Direction4.Right
                : Math.Sign(dir.y) == -1
                    ? Direction4.Down
                    : Direction4.Up;
        public static Direction4 From(this Vector2Int dir) => From((Vector2)dir);

        public static Vector2Int Random() {
            var x = (Direction4)UnityEngine.Random.Range(0, 4);
            return x.ToVector2Int();
        }
    }
}