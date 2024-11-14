using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Common.Utils {
    public static class ArrayEx {
        public static IEnumerable<string> GetObstacleNames(this RaycastHit2D[] hits, Transform yourself) => hits
            .Where(x => x.transform != null && x.transform != yourself)
            .Select(x => x.transform.name);

        public static string Recolor(this object obj, UnityEngine.Color color) => $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{obj}</color>";
        public static string Recolor(this object obj, System.Drawing.Color c) => $"<color=#{c.R:X2}{c.G:X2}{c.B:X2}>{obj}</color>";
    }
}