using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Inventory/Item")]
public class InventoryItem : ScriptableObject {
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
    public int numberHeld;
    public bool unique;
    public bool usable;
    public UnityEvent thisEvent;

    public void Use() {
        Debug.Log("Using " + itemName);
        thisEvent.Invoke();
    }

    public void DecreaseAmount(int amount = 1) {
        numberHeld = (numberHeld > 0) ? numberHeld - amount : 0;
    }
}
