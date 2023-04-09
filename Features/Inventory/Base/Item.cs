using System;
using UnityEngine.Events;

namespace InventorySystem {
    public class Item : IItem {
        public IItemInfo Info { get; }
        public IItemState State { get; }
        public Type Type => GetType();

        public Item(IItemInfo info) {
            Info = info;
            State = new ItemState();
        }
        public Item(IItemInfo info, IItemState state) {
            Info = info;
            State = state;
        }

        public IItem Clone() {
            var clonedItem = new Item(Info);
            clonedItem.State.Amount = State.Amount;

            return clonedItem;
        }
    }
}