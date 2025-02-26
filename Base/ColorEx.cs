using UnityEngine;

namespace DG_Pack.Base {
    public static class ColorEx {
        public static Color Alpha(this Color c, float a) => new(c.r, c.g, c.b, a);
        public static Color Grayscale(this Color c, float scale) => new(c.r * scale, c.g * scale, c.b * scale);

    }
}