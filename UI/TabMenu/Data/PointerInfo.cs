using System;

namespace Utility_Pack.UI.TabMenu.Data {
    public enum PointerState {
        Normal = 0, Hover = 1, Pressed = 2, Disabled = 3,
    }

    [Serializable]
    public class PointerInfo<T> {
        public T normal;
        public T hover;
        public T pressed;
        public T disabled;

        public PointerInfo() {
        }
        public PointerInfo(T value) {
            normal = hover = pressed = disabled = value;
        }
        public PointerInfo(PointerInfo<T> info) {
            normal = info.normal;
            hover = info.hover;
            pressed = info.pressed;
            disabled = info.disabled;
        }


        public T GetBy(PointerState state) => state switch {
            PointerState.Normal => normal,
            PointerState.Hover => hover,
            PointerState.Pressed => pressed,
            PointerState.Disabled => disabled,
            _ => normal,
        };
    }
}