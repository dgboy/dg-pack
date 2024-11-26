using UnityEngine;

namespace DGPack.Prototype {
    public class MoveToCell : MonoBehaviour {
        public float speed = 5.0f;
        public float cellSize = 1.0f;
        private bool _isMoving;
        private Vector3 _direction;
        private Vector2Int _target;

        private void Update() {
            if (_isMoving) {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, _target, step);

                if (Vector2.Distance(transform.position, _target) < 0f)
                    _isMoving = false;
            } else {
                _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                // _direction = new Vector3(_direction.x > 0f ? 1f : 0, _direction.y > 0f ? 1f : 0);
                    
                if (_direction == Vector3.zero)
                    return;

                var pos = transform.position + _direction * cellSize;
                _target = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
                _isMoving = true;
            }
        }
    }
}