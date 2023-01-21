using UnityEngine;
using UnityEngine.EventSystems;

namespace Inputs {
    // Pinch to zoom
    public class Zoomable : MonoBehaviour, IBeginDragHandler, IDragHandler {
#if UNITY_EDITOR || UNITY_STANDALONE
        [SerializeField] private PointerEventData.InputButton mouseButton = PointerEventData.InputButton.Right;
#endif

        [SerializeField] private bool useThis = true;
        [SerializeField] private RectTransform _target;
        [SerializeField] private float speed = 0.1f;
        [SerializeField] private float minBound = 0.1f;
        [SerializeField] private float maxBound = 10f;
        private Vector3 lastScale;
        private Vector2 lastPosition;
        public RectTransform Target { get => _target; set => _target = value; }


        private void Awake() {
            if (useThis && !_target) _target = transform as RectTransform;
        }

        //public void OnPointerDown(PointerEventData eventData) {
        //}

        public void OnBeginDrag(PointerEventData eventData) {
            if (!_target) return;

#if UNITY_EDITOR || UNITY_STANDALONE
            if (eventData.button != mouseButton) return;
            lastPosition = eventData.position;
            lastScale = _target.localScale;
#endif
        }

        public void OnDrag(PointerEventData eventData) {
            if (!_target) return;

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if (Input.touchCount < 2) return;
            lastScale = _target.localScale;
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);
            float distance0 = (t0.position - t0.deltaPosition).magnitude - (t1.position - t1.deltaPosition).magnitude;
            float distance1 = t0.position.magnitude - t1.position.magnitude;
            float zoomSize = distance0 - distance1;
#else
            if (eventData.button != mouseButton) return;
            float zoomSize = lastPosition.magnitude - eventData.position.magnitude;
#endif

            var scale = lastScale + speed * zoomSize * Vector3.one;
            var clampX = Mathf.Clamp(scale.x, minBound, maxBound);
            var clampY = Mathf.Clamp(scale.y, minBound, maxBound);
            _target.localScale = new Vector3(clampX, clampY, lastScale.z);
            //Debug.Log($"size: {zoomSize}, zoom: {new Vector3(clampX, clampY, lastScale.z)}");
        }

        public void SetTarget(RectTransform target) {
            _target = target;
        }
    }
}
