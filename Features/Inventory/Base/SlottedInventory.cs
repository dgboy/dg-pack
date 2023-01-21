using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem {
    public class SlottedInventory : IInventory {
        public event Action<object, IItem, int> OnItemAddedEvent;
        public event Action<object, Type, int> OnItemRemovedEvent;

        public int Capacity { get; set; }

        public bool IsFull => _slots.All(slot => slot.IsFull);
        private List<ISlot> _slots;

        public SlottedInventory(int capacity) {
            Capacity = capacity;

            _slots = new List<ISlot>(capacity);
            for (int i = 0; i < Capacity; i++) {
                _slots.Add(new Slot());
            }
        }


        public IItem GetItem(Type itemType) {
            return _slots.Find(slot => slot.ItemType == itemType).Item;
        }
        public IItem[] GetAllItems() {
            var allItems = new List<IItem>();

            foreach (var slot in _slots) {
                if (!slot.IsEmpty) allItems.Add(slot.Item);
            }

            return allItems.ToArray();
        }
        public IItem[] GetAllItems(Type itemType) {
            var itemsOfType = new List<IItem>();
            var requiredSlots = GetAllSlots(itemType);

            foreach (var slot in requiredSlots) {
                itemsOfType.Add(slot.Item);
            }

            return itemsOfType.ToArray();
        }
        public IItem[] GetEquippedItems() {
            var equippedItems = new List<IItem>();
            var requiredSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.Item.State.IsEquipped);

            foreach (var slot in requiredSlots) {
                equippedItems.Add(slot.Item);
            }

            return equippedItems.ToArray();
        }
        public int GetItemAmount(Type itemType) {
            int amount = 0;
            var requiredSlots = GetAllSlots(itemType);

            foreach (var slot in requiredSlots) amount += slot.Amount;

            return amount;
        }

        public bool TryAdd(object sender, IItem item) {
            var existSlot = _slots.Find(slot => !slot.IsEmpty && slot.ItemType == item.Type && !slot.IsFull);
            if (existSlot != null) return TryAddToSlot(sender, existSlot, item);

            var emptySlot = _slots.Find(slot => !slot.IsEmpty);
            if (emptySlot != null) return TryAddToSlot(sender, emptySlot, item);

            Debug.Log($"Inventory is full. Cannot add: {item}.");
            return false;
        }
        private bool TryAddToSlot(object sender, ISlot slot, IItem item) {
            bool fits = slot.Amount + item.State.Amount <= item.Info.Capacity;
            int income = fits ? item.State.Amount : item.Info.Capacity - slot.Amount;
            int remains = item.State.Amount - income;

            var clonedItem = item.Clone();
            clonedItem.State.Amount = income;

            if (slot.IsEmpty) {
                slot.SetItem(clonedItem);
            } else {
                slot.Item.State.Amount += income;
            }

            Debug.Log($"Item {item.Type} ({item.State.Amount}) added to inventory.");
            OnItemAddedEvent?.Invoke(sender, item, income);

            if (remains <= 0) return true;
            return TryAdd(sender, item);
        }

        public bool HasItem(Type itemType, out IItem item) {
            item = GetItem(itemType);
            return item != null;
        }

        public void Remove(object sender, Type itemType, int amount = 1) {
            var requiredSlots = GetAllSlots(itemType);
            if (requiredSlots.Length == 0) return;

            var amountOfLoss = amount;

            foreach (var slot in requiredSlots) {
                if (slot.Amount >= amountOfLoss) {
                    slot.Item.State.Amount -= amountOfLoss;

                    if (slot.Amount <= 0) slot.Clear();

                    Debug.Log($"Item {itemType} ({amount}) removed from inventory.");
                    OnItemRemovedEvent?.Invoke(sender, itemType, amountOfLoss);
                    break;
                }

                int removedAmount = slot.Amount;
                amountOfLoss -= slot.Amount;
                slot.Clear();
                OnItemRemovedEvent?.Invoke(sender, itemType, removedAmount);
            }
        }


        private ISlot[] GetAllSlots(Type itemType) {
            return _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).ToArray();
        }
    }
}