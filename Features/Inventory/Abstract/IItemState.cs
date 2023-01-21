using System;
using System.Collections.Generic;

namespace InventorySystem {
    public interface IItemState {
        int Amount { get; set; }
        bool IsEquipped { get; set; }
    }
}