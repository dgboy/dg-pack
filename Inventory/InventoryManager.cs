using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InventorySystem {
    public class InventoryManager : MonoBehaviour {
        public PlayerInventory playerInventory;
        public InventoryItem currentItem;

        [Header("Inventory Information")]
        [SerializeField] private GameObject blankInventorySlot = null;
        [SerializeField] private GameObject inventoryPanel = null;
        [SerializeField] private TextMeshProUGUI itemNameText = null;
        [SerializeField] private TextMeshProUGUI descriptionText = null;
        [SerializeField] private GameObject useButton = null;

        void OnEnable() {
            ClearInventorySlots();
            MakeInventorySlots();
            SetTextAndButton("", false);
        }

        void SetTextAndButton(string description, bool buttonActive) {
            descriptionText.text = description;
            itemNameText.text = description;
            if (buttonActive) {
                useButton.SetActive(true);
            } else {
                useButton.SetActive(false);
            }
        }

        void MakeInventorySlots() {
            if (playerInventory) {
                for (int i = 0; i < playerInventory.myInventory.Count; i++) {
                    Debug.Log(playerInventory.myInventory[i]);
                    if (playerInventory.myInventory[i].numberHeld > 0) {
                        GameObject temp = Instantiate(
                            blankInventorySlot,
                            inventoryPanel.transform.position,
                            Quaternion.identity
                        );

                        temp.transform.SetParent(inventoryPanel.transform);
                        Slot newSlot = temp.GetComponent<Slot>();

                        if (newSlot) {
                            newSlot.Setup(playerInventory.myInventory[i], this);
                        }


                        if (i == 0) {
                            Button button = temp.GetComponent<Button>();
                            button.Select();
                            // var eventSystem = EventSystemManager.currentSystem;
                            // eventSystem.SetSelectedGameObject(temp, new BaseEventData(eventSystem));
                        }
                    }
                }
            }
        }

        public void SetupDesciptionAndButton(string newDescription, string newItemNameText, bool isButtonUsable, InventoryItem newItem) {
            currentItem = newItem;
            descriptionText.text = newDescription;
            itemNameText.text = newItemNameText;
            useButton.SetActive(isButtonUsable);
        }

        public void UseButtonPressed() {
            if (currentItem) {
                currentItem.Use();
                ClearInventorySlots();
                MakeInventorySlots();
            }
        }

        void ClearInventorySlots() {
            for (int i = 0; i < inventoryPanel.transform.childCount; i++) {
                Destroy(inventoryPanel.transform.GetChild(i).gameObject);
            }
        }
    }
}