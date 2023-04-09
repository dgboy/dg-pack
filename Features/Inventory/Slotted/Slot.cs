using System;

namespace InventorySystem {
    public class Slot : ISlot {
        public bool IsFull => !IsEmpty && Amount == Capacity;
        public bool IsEmpty => Item == null;

        public IItem Item { get; private set; }
        public Type ItemType => Item.Type;
        public int Amount => IsEmpty ? 0 : Item.State.Amount;
        public int Capacity { get; private set; }


        public void SetItem(IItem item) {
            if (!IsEmpty) return;

            Item = item;
            Capacity = item.Info.Capacity;
        }

        public void Clear() {
            if (!IsEmpty) return;
            Item.State.Amount = 0;
            Item = null;
        }
    }
}