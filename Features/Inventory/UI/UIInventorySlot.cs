using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem {
    public class UIInventorySlot : UISlot {
        public event Action<ISlot, ISlot> OnItemDroppedEvent;
        [SerializeField] private Image image;
        [SerializeField] private UIInventoryItem itemUI;
        public ISlot Slot { get; set; }


        public override void OnDrop(PointerEventData eventData) {
            base.OnDrop(eventData);
            var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
            var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();

            Refresh();
            otherSlotUI.Refresh();
            OnItemDroppedEvent?.Invoke(Slot, otherSlotUI.Slot);
        }

        private void Refresh() {
            // if (slot != null) _itemUI.Refresh(slot);
        }
    }
}
