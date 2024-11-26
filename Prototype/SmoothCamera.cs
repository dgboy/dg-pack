using UnityEngine;

namespace DGPack.Prototype {
    public class SmoothCamera : MonoBehaviour {
        public float smoothing = 0.1f;

        public Transform Target { get; set; }
        public Vector2 minPos;
        public Vector2 maxPos;


        private void Start() {
            //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

        private void LateUpdate() {
            if (Vector2.Distance(transform.position, Target.position) > 0f) {
                Follow();
            }
        }

        private void Follow() {
            var to = new Vector3(Target.position.x, Target.position.y, transform.position.z);
            //targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            //targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
            transform.position = to;//Vector3.Lerp(transform.position, to, smoothing);
        }
    }
}