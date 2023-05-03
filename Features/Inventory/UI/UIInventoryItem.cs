using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem {
    public class UIInventoryItem : UIItem {
        [SerializeField] private Image _imageIcon;
        [SerializeField] private TextMeshProUGUI _labelAmount;

        public IItem Item { get; private set; }

    
        public void Refresh(IItem item) {
            Item = item;
            _imageIcon.sprite = Item.Info.Icon;
            _labelAmount.gameObject.SetActive(true);

            bool showAmount = Item.State.Amount > 1;
            _labelAmount.gameObject.SetActive(showAmount);

            if (showAmount) _labelAmount.text = $"x{Item.State.Amount}";
        }

        private void Cleanup() {
            _labelAmount.gameObject.SetActive(false);
            _imageIcon.gameObject.SetActive(false);
        }
    }
}