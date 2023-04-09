using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    public class EquipmentItem : IItem {
        public IItemInfo Info { get; }
        public IItemState State { get; }
        public Type Type { get; }

        public EquipmentItem(IItemInfo info) {
            Info = info;
            State = new ItemState();
        }
        
        public IItem Clone() {
            var clone = new EquipmentItem(Info);
            clone.State.Amount = State.Amount;
            return clone;
        }
    }
}
