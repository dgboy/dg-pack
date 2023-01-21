using UnityEngine;

namespace InventorySystem {
    public interface IItemInfo {
        string ID { get; }
        string Title { get; }
        string Description { get; }
        Sprite Icon { get; }
        int Capacity { get; }
    }
}