using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem {
    public class SlottedInventory : IInventory {
        public event Action<object, IItem, int> OnItemAddedEvent;
        public event Action<object, Type, int> OnItemRemovedEvent;
        public event Action<object> OnStateChangedEvent;

        public int Capacity { get; }
        public bool IsFull => Slots.All(slot => slot.IsFull);
        public List<ISlot> Slots { get; }

        public SlottedInventory(int capacity) {
            Capacity = capacity;

            Slots = new List<ISlot>(capacity);
            for (int i = 0; i < Capacity; i++)
                Slots.Add(new Slot());
        }
        public SlottedInventory(int capacity, IEnumerable<IItem> items) : this(capacity) {
            foreach (var item in items)
                Slots[item.State.Index].SetItem(item);
        }


        public IItem GetItem(Type itemType) {
            return Slots.Find(slot => slot.ItemType == itemType).Item;
        }
        public IItem[] GetAllItems() {
            return (from slot in Slots where !slot.IsEmpty select slot.Item).ToArray();
        }
        public IItem[] GetAllItems(Type itemType) {
            var requiredSlots = GetAllSlots(itemType);
            return requiredSlots.Select(slot => slot.Item).ToArray();
        }
        public IItem[] GetEquippedItems() {
            var requiredSlots = Slots.FindAll(slot => !slot.IsEmpty && slot.Item.State.IsEquipped);
            return requiredSlots.Select(slot => slot.Item).ToArray();
        }
        public int GetItemAmount(Type itemType) {
            var requiredSlots = GetAllSlots(itemType);
            return requiredSlots.Sum(slot => slot.Amount);
        }

        private ISlot[] GetAllSlots(Type itemType) {
            return Slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).ToArray();
        }

        public bool HasItem(Type itemType, out IItem item) {
            item = GetItem(itemType);
            return item != null;
        }


        public bool TryAdd(object sender, IItem item) {
            var existSlot = Slots.Find(slot => !slot.IsEmpty && slot.ItemType == item.Type && !slot.IsFull);
            if (existSlot != null) return TryAddToSlot(sender, existSlot, item);

            var emptySlot = Slots.Find(slot => !slot.IsEmpty);
            if (emptySlot != null) return TryAddToSlot(sender, emptySlot, item);

            Debug.Log($"Inventory is full. Cannot add: {item}.");
            return false;
        }
        public bool TryAddToSlot(object sender, ISlot slot, IItem item) {
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
            OnStateChangedEvent?.Invoke(this);

            return remains <= 0 || TryAdd(sender, item);
        }

        public void TransitBetweenSlots(object sender, ISlot fromSlot, ISlot toSlot) {
            if (fromSlot.IsEmpty) return;
            if (toSlot.IsFull) return;
            if (!toSlot.IsEmpty && fromSlot.ItemType != toSlot.ItemType) return;

            int capacity = fromSlot.Capacity;
            bool fits = fromSlot.Amount + toSlot.Amount <= capacity;
            int income = fits ? fromSlot.Amount : capacity - toSlot.Amount;
            int remains = fromSlot.Amount - income;

            if (toSlot.IsEmpty) {
                toSlot.SetItem(fromSlot.Item);
            } else {
                toSlot.Item.State.Amount += income;
            }

            if (fits) fromSlot.Clear();
            else fromSlot.Item.State.Amount = remains;

            OnStateChangedEvent?.Invoke(this);
        }

        public void Remove(object sender, Type itemType, int amount = 1) {
            var requiredSlots = GetAllSlots(itemType);
            if (requiredSlots.Length == 0) return;

            int amountOfLoss = amount;

            foreach (var slot in requiredSlots) {
                if (slot.Amount >= amountOfLoss) {
                    slot.Item.State.Amount -= amountOfLoss;

                    if (slot.Amount <= 0) slot.Clear();

                    Debug.Log($"Item {itemType} ({amount}) removed from inventory.");
                    OnItemRemovedEvent?.Invoke(sender, itemType, amountOfLoss);
                    OnStateChangedEvent?.Invoke(this);
                    break;
                }

                int removedAmount = slot.Amount;
                amountOfLoss -= slot.Amount;
                slot.Clear();
                OnStateChangedEvent?.Invoke(this);
                OnItemRemovedEvent?.Invoke(sender, itemType, removedAmount);
            }
        }
    }
}