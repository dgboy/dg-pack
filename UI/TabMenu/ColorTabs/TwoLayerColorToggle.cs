using System;
using DG_Pack.UI.TabMenu.Base;
using DG_Pack.UI.TabMenu.Data;
using UnityEngine;
using UnityEngine.UI;

namespace DG_Pack.UI.TabMenu.ColorTabs {
    public class TwoLayerColorToggle : Tab {
        public event Action<bool> OnChanged;
        [Header("Display")]
        [SerializeField] private Graphic pointerBox;
        [SerializeField] private Graphic firstLayer;
        [SerializeField] private Graphic secondLayer;

        [Header("Colors")]
        public PointerInfo<Color> pointerColors = new(PointerInfoPalette.Grayscale);
        public SelectionInfo<Color> firstColors = new(Color.white, Color.black);
        public SelectionInfo<Color> secondColors = new(Color.black, Color.white);


        protected override void SetState(PointerState state) {
            pointerState = state;
            pointerBox.CrossFadeColor(pointerColors.GetBy(state), 0, true, true);
        }
        public override void Select(bool on, bool sendEvent = false) {
            selected = on && Active;

            firstLayer.color = firstColors.GetBy(selected);
            secondLayer.color = secondColors.GetBy(selected);
            if (sendEvent) OnChanged?.Invoke(on);
        }
    }
}