using UnityEngine;
using UnityEngine.UI;
using Utility_Pack.UI.TabMenu.Data;

namespace Utility_Pack.UI.TabMenu.ColorTabs {
    public class DoubleTab : Tab {
        [Header("Display")]
        [SerializeField] private Graphic pointerBox;
        [SerializeField] private Graphic[] selectionBox;

        [Header("Colors")]
        public PointerInfo<Color> pointerInfo = new(PointerInfoPalette.Grayscale);
        public SelectionInfo<Color> selectionInfo = new(Color.white);


        protected override void SetState(PointerState state) {
            pointerState = state;
            pointerBox.CrossFadeColor(pointerInfo.GetBy(state), 0, true, true);
        }
        public override void Select(bool on, bool sendEvent = false) {
            selected = on && Active;

            foreach (var selectGraphic in selectionBox) 
                selectGraphic.color = selectionInfo.GetBy(selected);
        }
    }
}