using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

public class InventoryTester : MonoBehaviour {
    [Header("UI")]
    [SerializeField] private UIInventory _inventoryUI;

    [Header("General")]
    [SerializeField] private int slotAmount = 20;
    [SerializeField] private ItemInfo[] itemInfos;
    [SerializeField] private ItemState[] itemStates;
    private List<IItem> _items;

    public SlottedInventory Inventory { get; private set; }

    private void Start() {
        _items = new List<IItem>();
        for (int i = 0; i < itemInfos.Length; i++) {
            _items.Add(new Item(itemInfos[i], itemStates[i]));
        }

        Inventory = new SlottedInventory(slotAmount, _items.ToArray());
        _inventoryUI.Init(Inventory);
        // AddRandomItems();
    }

    private ISlot FillInventoryRandomly(List<ISlot> slots) {
        int slotIdx = Random.Range(0, slots.Count);
        var slot = slots[slotIdx];
        int amount = Random.Range(1, 4);
        var item = new Item(itemInfos[0], new ItemState(amount));
        Inventory.TryAddToSlot(this, slot, item);
        return slot;
    }
}
