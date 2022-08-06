using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    // [CreateAssetMenu(menuName = "ScriptableObjects/Inventory", fileName = "Inventory")]
    public class Inventory : ScriptableObject, ISerializationCallbackReceiver {

        public global::ItemOld currentItem;
        public List<global::ItemOld> items = new List<global::ItemOld>();
        public int numberOfKeys = 0;
        public int coins = 0;
        public float maxMagic = 10;
        public float currentMagic;

        public void OnEnable() {
            currentMagic = maxMagic;
        }

        public void DecreaseMagic(float magicCost) {
            currentMagic -= magicCost;
        }

        public bool CheckForItem(global::ItemOld item) {
            if (items.Contains(item)) {
                return true;
            }
            return false;
        }

        public void OnAfterDeserialize() {
            numberOfKeys = 0;
            coins = 0;
        }
        public void OnBeforeSerialize() {

        }

        public void AddItem(global::ItemOld itemToAdd) {
            if (itemToAdd.isKey) {
                numberOfKeys++;
            } else {
                if (!items.Contains(itemToAdd)) {
                    items.Add(itemToAdd);
                }
            }
        }
    }
}