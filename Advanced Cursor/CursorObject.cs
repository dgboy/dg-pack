using UnityEngine;
using UnityEngine.EventSystems;

namespace AdvancedCursor {
    public class CursorObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private CursorManager.Type cursorType;

        // private void Awake() {
        // }

        public void OnPointerEnter(PointerEventData eventData) {
            Debug.Log("OnPointerEnter");
            CursorManager.self.SetCursor(cursorType);
        }

        public void OnPointerExit(PointerEventData eventData) {
            Debug.Log("OnPointerExit");
            CursorManager.self.SetDefaultCursor();
        }

        // public void OnPointerClick(PointerEventData eventData) {
        //     Debug.Log("OnPointerEnter");
        //     CursorManager.self.SetActiveCursor(cursorType);
        // }

        // private void OnDestroy() {
        // }
    }
}