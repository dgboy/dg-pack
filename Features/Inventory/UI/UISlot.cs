using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        var other = eventData.pointerDrag.transform;
        other.SetParent(transform);
        other.localPosition = Vector3.zero;
    }
}
