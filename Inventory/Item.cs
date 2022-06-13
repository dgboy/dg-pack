using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "ScriptableObjects/Item", fileName = "Item")]
public class Item : ScriptableObject {

    public Sprite itemSprite;
    public string itemDescription;
    public bool isKey;
}
