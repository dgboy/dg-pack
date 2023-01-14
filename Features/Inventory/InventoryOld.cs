using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryOld : ScriptableObject, ISerializationCallbackReceiver {

    public ItemOld currentItem;
    public List<ItemOld> items = new List<ItemOld>();
    public int numberOfKeys = 0;
    public int coins = 0;

    public Vector2 defaultValue;

    public void OnAfterDeserialize() {
        numberOfKeys = 0;
        coins = 0;
    }
    public void OnBeforeSerialize() {
        
    }

    public void AddItem(ItemOld itemToAdd) {
        if(itemToAdd.isKey) {
            numberOfKeys++;
        } else {
            if(!items.Contains(itemToAdd)) {
                items.Add(itemToAdd);
            }
        }
    }
}
