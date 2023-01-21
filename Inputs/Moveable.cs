using UnityEngine;
using UnityEngine.EventSystems;

namespace Inputs {
    /// <summary>
    /// Drag (Click / Touch)
    /// </summary>
    public class Moveable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
#if UNITY_EDITOR || UNITY_STANDALONE
        [SerializeField] private PointerEventData.InputButton mouseButton = PointerEventData.InputButton.Left;
#endif

        [SerializeField] private bool isWorldSpace = true;
        [SerializeField] private bool useThis = true;
        [SerializeField] private Transform _target;
        private Vector2 lastPosition;
        public Transform Target { get => _target; set => _target = value; }


        private void Awake() {
            if (useThis && !_target) _target = transform;
        }

        public void SetTarget(Transform target) {
            _target = target;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (!_target) return;
            lastPosition = Convert(eventData.position);
        }

        public void OnDrag(PointerEventData eventData) {
            if (!_target) return;

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if (Input.touchCount != 1) lastPosition = Vector2.zero;
            Vector2 currentPosition = Input.GetTouch(0).position;
#else
            if (eventData.button != mouseButton) return;
            Vector2 currentPosition = Convert(eventData.position);
#endif
            if (lastPosition != Vector2.zero) {
                Vector2 diff = currentPosition - lastPosition;
                Vector3 newPosition = _target.position + new Vector3(diff.x, diff.y, 0);
                _target.position = newPosition;
                lastPosition = currentPosition;
            }
        }
        public void OnEndDrag(PointerEventData eventData) {
            if (!_target) return;
            //lastPosition = currentPosition = Vector2.zero;
        }

        private Vector3 Convert(Vector3 position) => isWorldSpace ? Camera.main.ScreenToWorldPoint(position) : position;
    }
}