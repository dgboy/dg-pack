using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {
    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    [Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberText = null;
    [SerializeField] private Image itemImage = null;

    public void Setup(InventoryItem newItem, InventoryManager newManager) {
        thisItem = newItem;
        thisManager = newManager;

        if (thisItem) {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = (thisItem.unique) ? "" : "" + thisItem.numberHeld;
        }
    }

    public void ClickOn() {
        if (thisItem) {
            thisManager.SetupDesciptionAndButton(thisItem.itemDescription, thisItem.itemName, thisItem.usable, thisItem);
        }
    }
}
