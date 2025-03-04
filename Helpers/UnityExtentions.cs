using UnityEngine;

namespace DG_Pack.Helpers {
    public static class TransformEx {
        public static Transform Clear(this Transform transform) {
            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
            return transform;
        }
        public static void SetInPercent(this Transform transform, Vector2 percent, Vector2 parentSize) {
            var pos = percent;
            pos.x = Mathf.Clamp(pos.x * parentSize.x - parentSize.x / 2, -parentSize.x / 2, parentSize.x / 2);
            pos.y = Mathf.Clamp(pos.y * parentSize.y - parentSize.y / 2, -parentSize.y / 2, parentSize.y / 2);
            transform.localPosition = pos;
        }
    }

    public static class GameObjectEx {
        public static void Toggle(this GameObject gameObject) {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public static class VectorEx {
        public static void SetVectorArray(this Material mat, string name, Vector2[] array) {
            Vector4[] ex = new Vector4[array.Length];
            for (int i = 0; i < array.Length; i++) {
                ex[i] = new Vector4(array[i].x, array[i].y);
            }
            mat.SetVectorArray(name, ex);
        }

        public static Vector2[] SetOriginToZero(this Vector2[] array) {
            float[] xs = new float[array.Length];
            float[] ys = new float[array.Length];
            for (int i = 0; i < array.Length; i++) {
                xs[i] = array[i].x;
                ys[i] = array[i].y;
            }

            var result = new Vector2[array.Length];
            var min = new Vector2(Mathf.Min(xs), Mathf.Min(ys));
            for (int i = 0; i < array.Length; i++) {
                result[i] = array[i] - min;
            }

            //result.Display();
            return result;
        }

        public static Vector2 GetBoundSize(this Vector2[] array) {
            float[] xs = new float[array.Length];
            float[] ys = new float[array.Length];
            for (int i = 0; i < array.Length; i++) {
                xs[i] = array[i].x;
                ys[i] = array[i].y;
            }
            var max = new Vector2(Mathf.Max(xs), Mathf.Max(ys));
            var min = new Vector2(Mathf.Min(xs), Mathf.Min(ys));

            Debug.Log($"[rect size] max: {max}, min: {min}");
            //return new Vector2(Mathf.Abs(max.x - min.x), Mathf.Abs(max.y - min.y));
            return new Vector2(Mathf.Abs(max.x - min.x), Mathf.Abs(max.y - min.y));
        }
        public static Vector2 GetRectSize(this Vector2[] a) {
            var w = Mathf.Abs(a[0].x + a[1].x + a[2].x + a[3].x) / 2;
            var h = Mathf.Abs(a[0].y + a[1].y + a[2].y + a[3].y) / 2;
            var size = new Vector2(w, h);
            Debug.Log($"[rect size] size: {size}");
            return size;
        }

        public static void Display(this Vector2[] array, string prefix = "test") {
            string msg = $"[{prefix}] Vector2[{array.Length}] ";
            for (int i = 0; i < array.Length; i++) {
                msg += $"{array[i]:f2}; ";
            }

            Debug.Log(msg);
        }


        public static void SetPositions(this LineRenderer line, Vector2[] points, Vector3 offset) {
            var positions = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) {
                positions[i] = new Vector3(points[i].x, points[i].y) + offset;
                //positions[i + ((i == 2) ? 1 : (i == 3) ? -1 : 0)] = new Vector3(points[i].x, points[i].y);
            }

            line.SetPositions(positions);
        }

        /// <summary>
        /// Get aspect ratio of Vector2
        /// </summary>
        public static float GetAR(this Vector2 vector) => vector.x / vector.y;
        public static float GetAR(this Vector2Int vector) => vector.x / (float)vector.y;
    }
}