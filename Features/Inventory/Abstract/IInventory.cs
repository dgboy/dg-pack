using System;

namespace InventorySystem {
    public interface IInventory {
        int Capacity { get; }
        bool IsFull { get; }

        IItem GetItem(Type itemType);
        IItem[] GetAllItems();
        IItem[] GetAllItems(Type itemType);
        IItem[] GetEquippedItems();
        int GetItemAmount(Type itemType);

        bool TryAdd(object sender, IItem item);
        bool HasItem(Type itemType, out IItem item);
        void Remove(object sender, Type itemType, int amount = 1);
    }
}