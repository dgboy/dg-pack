using UnityEngine;
using UnityEngine.EventSystems;
using Utility_Pack.UI.TabMenu.Base;
using Utility_Pack.UI.TabMenu.Data;

namespace Utility_Pack.UI.TabMenu {
    public abstract class Tab : MonoBehaviour, ITab, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
        [Header("States")]
        [SerializeField] protected PointerState pointerState = PointerState.Normal;
        [SerializeField] protected bool selected;
        private ITabMenu Group { get; set; }

        public bool Active {
            get => enabled;
            set {
                enabled = value;
                SetState(PointerState.Disabled);
            }
        }


        private void Start() => SetState(Active ? PointerState.Normal : PointerState.Disabled);

        public void OnPointerEnter(PointerEventData eventData) => SetState(PointerState.Hover);
        public void OnPointerExit(PointerEventData eventData) => SetState(PointerState.Normal);

        public void OnPointerDown(PointerEventData eventData) => SetState(PointerState.Pressed);
        public void OnPointerUp(PointerEventData eventData) {
            selected = !selected;

            if (Group == null) Select(selected, true);
            else Group.Select(this);
        }

        public void SetGroup(ITabMenu value) => Group = value;
        protected abstract void SetState(PointerState state);
        public abstract void Select(bool on, bool sendEvent = false);
    }
}