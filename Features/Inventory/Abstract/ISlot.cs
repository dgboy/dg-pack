using System;

namespace InventorySystem {
    public interface ISlot {
        bool IsFull { get; }
        bool IsEmpty { get; }

        IItem Item { get; }
        Type ItemType { get; }
        int Amount { get; }
        int Capacity { get; }

        void SetItem(IItem item);
        void Clear();
    }
}