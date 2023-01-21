using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem {
    [CreateAssetMenu(fileName = "Item", menuName = "Content/Inventory/Item")]
    public class ItemInfo : ScriptableObject, IItemInfo {
        [SerializeField] private string _id;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _capacity;

        public string ID => _id;
        public string Title => _title;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int Capacity => _capacity;
    }
}