using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class MouseGizmo : MonoBehaviour {
        private void OnDrawGizmos() {
            if (!Input.GetMouseButton(0)) return;

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.2f);
        }
    }
}