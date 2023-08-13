using UnityEngine;

namespace Utility_Pack.UI.TabMenu.Data {
    public static class PointerInfoPalette {
        public static readonly PointerInfo<Color> Grayscale = new() {
            normal = new Color(1.0f, 1.0f, 1.0f, 1.0f),
            hover = new Color(0.9f, 0.9f, 0.9f, 1.0f),
            pressed = new Color(0.8f, 0.8f, 0.8f, 1.0f),
            disabled = new Color(0.6f, 0.6f, 0.6f, 0.6f),
        };
    }
}