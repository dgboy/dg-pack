using System.Collections.Generic;
using UnityEngine;

namespace Core.Common.Utils {
    public static class CommonEx {
        public static T GetAny<T>(this IReadOnlyList<T> data) => data[Random.Range(0, data.Count)];
    }
}