using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class Agent : MonoBehaviour {
        public float speed = 5f;
        public int targetIndex;
        public List<Vector3> path;

        private void Update() {
            if (path is not { Count: > 0 })
                return;

            var waypoint = path[targetIndex];
            transform.position = Vector3.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);

            if (!(Vector3.Distance(transform.position, waypoint) < 0.1f))
                return;

            targetIndex++;

            if (targetIndex >= path.Count)
                path = null;
        }


        public void SetPath(List<Vector3> newPath) {
            path = newPath;
            targetIndex = 0;
        }
    }
}