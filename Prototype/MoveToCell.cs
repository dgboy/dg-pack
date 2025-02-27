using DG_Pack.Services.Log;
using UnityEngine;
using UnityEngine;

namespace DG_Pack.Prototype {
    public class MoveToCell : MonoBehaviour {
        public float speed = 5.0f;
        public float cellSize = 1.0f;
        private bool _isMoving;
        private Vector2 _direction;
        private Vector2Int _target;

        private void Update() {
            Vector2 position = transform.position;

            if (_isMoving) {
                var towards = Vector2.MoveTowards(position, _target, speed * Time.deltaTime);

                // if (position == towards)
                //     DLogger.Log($"{transform.name} stuck!", this);

                transform.position = towards;

                if (Vector2.Distance(position, _target) == 0f)
                    _isMoving = false;
            } else {
                _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                // _direction = new Vector3(_direction.x > 0f ? 1f : 0, _direction.y > 0f ? 1f : 0);

                if (_direction == Vector2.zero)
                    return;

                var pos = position + _direction * cellSize;
                _target = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
                _isMoving = true;
            }
        }
    }
}