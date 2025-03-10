﻿using UnityEngine;

namespace DG_Pack.Prototype {
    public class CameraController : MonoBehaviour {
        public Transform target;

        public float smoothSpeed = 0.125f;

        public bool useBounds = true;
        public BoxCollider2D boundaryCollider;
        private float minX, maxX, minY, maxY;

        public bool useDeadzone;
        public Vector2 deadzoneSize = new(2f, 2f);

        private Vector3 _velocity = Vector3.zero;
        private Camera _camera;


        private void OnValidate() => UpdateBoundsFromCollider();

        private void Start() {
            _camera = GetComponent<Camera>();
            UpdateBoundsFromCollider();
        }

        private void LateUpdate() {
            if (target == null) return;

            Vector3 targetPosition = CalculateTargetPosition();
            Vector3 smoothedPosition = ApplySmoothing(targetPosition);

            if (useBounds) smoothedPosition = ApplyCameraBounds(smoothedPosition);

            transform.position = smoothedPosition;
        }

        private Vector3 CalculateTargetPosition() {
            Vector3 result = target.position;
            result.z = transform.position.z;

            if (useDeadzone) {
                Vector3 cameraPos = transform.position;
                Vector3 delta = result - cameraPos;

                // Проверка и коррекция по X
                if (Mathf.Abs(delta.x) > deadzoneSize.x / 2) {
                    float direction = Mathf.Sign(delta.x);
                    result.x = cameraPos.x + (delta.x - direction * deadzoneSize.x / 2);
                } else {
                    result.x = cameraPos.x;
                }

                // Проверка и коррекция по Y
                if (Mathf.Abs(delta.y) > deadzoneSize.y / 2) {
                    float direction = Mathf.Sign(delta.y);
                    result.y = cameraPos.y + (delta.y - direction * deadzoneSize.y / 2);
                } else {
                    result.y = cameraPos.y;
                }
            }

            return result;
        }

        private Vector3 ApplySmoothing(Vector3 targetPosition) {
            return Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref _velocity,
                smoothSpeed
            );
        }

        private Vector3 ApplyCameraBounds(Vector3 position) {
            if (_camera == null) return position;

            UpdateBoundsFromCollider();

            float cameraHeight = _camera.orthographicSize;
            float cameraWidth = cameraHeight * _camera.aspect;

            // Рассчитываем доступное пространство и требуемый размер
            float availableWidth = maxX - minX;
            float requiredWidth = cameraWidth * 2f;
            float availableHeight = maxY - minY;
            float requiredHeight = cameraHeight * 2f;

            // Обработка оси X
            if (availableWidth >= requiredWidth) {
                position.x = Mathf.Clamp(
                    position.x,
                    minX + cameraWidth,
                    maxX - cameraWidth
                );
            } else {
                position.x = (minX + maxX) * 0.5f; // Центрируем камеру
            }

            // Обработка оси Y
            if (availableHeight >= requiredHeight) {
                position.y = Mathf.Clamp(
                    position.y,
                    minY + cameraHeight,
                    maxY - cameraHeight
                );
            } else {
                position.y = (minY + maxY) * 0.5f; // Центрируем камеру
            }

            return position;
        }

        private void UpdateBoundsFromCollider() {
            if (boundaryCollider != null) {
                Bounds bounds = boundaryCollider.bounds;
                minX = bounds.min.x;
                maxX = bounds.max.x;
                minY = bounds.min.y;
                maxY = bounds.max.y;
            }
        }

        private void OnDrawGizmosSelected() {
            if (useDeadzone) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(transform.position, new Vector3(deadzoneSize.x, deadzoneSize.y, 0));
            }
        }
    }
}