using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InventorySystem;

public class NumberBar : MonoBehaviour {
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private TextMeshProUGUI label;

    public void UpdateCoinCount() {
        label.text = $"{playerInventory.Coins}";
    }
}
