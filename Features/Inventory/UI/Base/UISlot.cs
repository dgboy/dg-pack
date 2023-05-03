using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem {
    public class UISlot : MonoBehaviour, IDropHandler {
        public virtual void OnDrop(PointerEventData eventData) {
            var other = eventData.pointerDrag.transform;
            other.SetParent(transform);
            other.localPosition = Vector3.zero;
        }
    }
}
