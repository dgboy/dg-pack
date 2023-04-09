using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem {
    public class UIInventory : MonoBehaviour {
        [Header("General")]
        [SerializeField] private GridLayoutGroup gridGroup;
        [SerializeField] private RectTransform slotHolder;
        [SerializeField] private UIInventorySlot slotPrefab;
        [SerializeField] private UIInventoryItem itemPrefab;
        public SlottedInventory Inventory { get; private set; }
        public List<UIInventorySlot> Slots { get; private set; }


        public void Init(SlottedInventory inventory) {
            Inventory = inventory;
            GenerateSlots();
        }

        private async void GenerateSlots() {
            slotHolder.Clear();
            await Task.Yield();

            Slots = new List<UIInventorySlot>();
            foreach (var slot in Inventory.Slots) {
                var slotUI = Instantiate(slotPrefab, slotHolder);
                slotUI.name = $"slot {slotHolder.childCount}";
                slotUI.Slot = slot;

                if (!slot.IsEmpty) {
                    var itemUI = Instantiate(itemPrefab, slotUI.transform);
                    itemUI.Refresh(slot.Item);
                }

                slotUI.OnItemDroppedEvent += Refresh;
                Slots.Add(slotUI);
            }

            await Task.Yield();
            gridGroup.enabled = false;
        }

        private void Refresh(ISlot fromSlot, ISlot toSlot) {
            Inventory.TransitBetweenSlots(this, fromSlot, toSlot);
        }
    }
}
