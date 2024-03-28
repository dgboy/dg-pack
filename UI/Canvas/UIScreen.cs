using UnityEngine;

namespace UtilityPack.UI {
    public class UIScreen : MonoBehaviour {
        public virtual void Show() {
            gameObject.SetActive(true);
        }
        public virtual void Hide() {
            gameObject.SetActive(false);
        }

        public virtual void GoTo(UIScreen nextScreen) {
            Hide();
            nextScreen.Show();
        }
    }
}
