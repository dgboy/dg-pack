using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem {
    public class Slot : MonoBehaviour {
        [Header("Variables from the item")]
        public Item item;
        public InventoryManager manager;

        [Header("UI Stuff to change")]
        [SerializeField] private TextMeshProUGUI itemNumberText = null;
        [SerializeField] private Image itemImage = null;

        public void Setup(Item newItem, InventoryManager newManager) {
            item = newItem;
            manager = newManager;

            if (item) {
                itemImage.sprite = item.itemImage;
                itemNumberText.text = item.unique ? "" : "" + item.numberHeld;
            }
        }

        public void ClickOn() {
            if (item) {
                manager.SetupDesciptionAndButton(item.itemDescription, item.itemName, item.usable, item);
            }
        }
    }
}