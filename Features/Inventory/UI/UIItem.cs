using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    private Canvas _uiCanvas;
    private CanvasGroup _group;
    private RectTransform _rect;

    private void Start() {
        _rect = GetComponent<RectTransform>();
        _uiCanvas = GetComponentInParent<Canvas>();
        _group = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData) {
        _rect.parent.SetAsLastSibling();
        _group.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData) {
        _rect.anchoredPosition += eventData.delta / _uiCanvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData) {
        _rect.SetAsFirstSibling();
        _rect.anchoredPosition = Vector3.zero;
        _group.blocksRaycasts = true;
    }
}
