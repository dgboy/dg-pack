using UnityEngine;
using UnityEngine.UI;
using Utility_Pack.UI.TabMenu.Data;

namespace Utility_Pack.UI.TabMenu.ColorTabs {
    public class ColorTab : Tab {
        [Header("Display")]
        [SerializeField] protected Graphic graphic;

        [Header("Colors")]
        public PointerInfo<Color> pointerInfo = new(PointerInfoPalette.Grayscale);
        public SelectionInfo<Color> selectionInfo = new(Color.white);
        private bool IsSelected => selected && pointerState is PointerState.Normal or PointerState.Hover;


        protected override void SetState(PointerState state) {
            pointerState = Active ? state : PointerState.Disabled;

            var color = IsSelected ? selectionInfo.GetBy(selected) : pointerInfo.GetBy(pointerState);
            graphic.CrossFadeColor(color, 0f, true, true);
        }

        public override void Select(bool on, bool sendEvent = false) {
            selected = on && Active;

            graphic.CrossFadeColor(selectionInfo.GetBy(selected), 0f, true, true);
        }
    }
}