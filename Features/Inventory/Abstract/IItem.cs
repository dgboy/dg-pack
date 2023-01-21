using System;

namespace InventorySystem {
    public interface IItem {
        IItemInfo Info { get; }
        IItemState State { get; }
        Type Type { get; }

        IItem Clone();
    }
}