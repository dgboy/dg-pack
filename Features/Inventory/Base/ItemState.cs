using UnityEngine;

namespace InventorySystem {
    [System.Serializable]
    public class ItemState : IItemState {
        [SerializeField] private int _amount;
        [SerializeField] private int _index;
        [SerializeField] private bool _isEquiped;

        public int Amount { get => _amount; set => _amount = value; }
        public int Index { get => _index; set => _index = value; }
        public bool IsEquipped { get => _isEquiped; set => _isEquiped = value; }

        public ItemState() {
            _amount = 0;
            _isEquiped = false;
        }
        public ItemState(int amount) {
            _amount = amount;
        }
    }
}