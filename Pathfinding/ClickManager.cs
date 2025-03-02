using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class ClickManager : MonoBehaviour {
        [SerializeField] private PathfinderHub hub;
        private Camera _camera;

        private void Awake() {
            _camera = Camera.main;
        }
        private void Update() {
            if (Input.GetMouseButtonDown(0))
                MoveToTarget();
        }


        private void MoveToTarget() {
            var current = hub.agent.transform.position;
            var target = _camera.ScreenToWorldPoint(Input.mousePosition);
            hub.agent.SetPath(hub.pathfinder.FindPath(current, target));
        }
    }
}