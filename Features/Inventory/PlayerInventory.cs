using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    [System.Serializable]
    [CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory/Inventory")]
    public class PlayerInventory : ScriptableObject {
        public List<Item> myInventory = new();
        public Item receiveItem;
        [SerializeField] private int coins = 0;
        // public int keys = 0;

        public int Coins { get => coins; set => coins = value; }


        public bool CheckForItem(Item item) {
            return myInventory.Contains(item);
        }

        public void AddItem(Item item) {
            receiveItem = item;

            if (CheckForItem(item)) {
                item.numberHeld += 1;
            } else {
                myInventory.Add(item);
                item.numberHeld += 1;
            }
            // if(item.isKey) {
            //     keys++;
            // } else {
            // }
        }
    }
}