using System;
using UnityEngine;

namespace Utility_Pack.UI.TabMenu.Data {
    public enum TabState {
        Normal = 0, Hover = 1, Pressed = 2, Disabled = 3, Selected = 4, Unselected = 5,
    }

    [Serializable]
    public class TabInfo<T> {
        public T normal;
        public T hover;
        public T pressed;
        public T disabled;
        public T selected;
        public T unselected;


        public T GetStateInfo(TabState state) => state switch {
            TabState.Normal => normal,
            TabState.Hover => hover,
            TabState.Pressed => pressed,
            TabState.Disabled => disabled,
            TabState.Selected => selected,
            TabState.Unselected => unselected,
            _ => normal,
        };

        public TabInfo() {
            // Normal = Hover = Pressed = Selected = Disabled = value;
        }
        public TabInfo(TabInfo<T> value) {
            normal = value.normal;
            hover = value.hover;
            pressed = value.pressed;
            selected = value.selected;
            disabled = value.disabled;
            unselected = value.unselected;
        }
    }

    public static class TabInfoPalette {
        public static readonly TabInfo<Color> Grayscale = new() {
            normal = new Color(1.0f, 1.0f, 1.0f, 1.0f),
            hover = new Color(0.9f, 0.9f, 0.9f, 1.0f),
            pressed = new Color(0.8f, 0.8f, 0.8f, 1.0f),
            selected = new Color(0.7f, 0.7f, 0.7f, 1.0f),
            disabled = new Color(0.6f, 0.6f, 0.6f, 0.6f),
            unselected = new Color(1.0f, 1.0f, 1.0f, 1.0f),
        };
    }
}