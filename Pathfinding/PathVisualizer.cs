using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Pathfinding {
// На камеру или объект с коллайдером добавьте:

    public class PathVisualizer : MonoBehaviour {
        [SerializeField] private PathfinderHub hub;
        private GridManagerBase Grid => hub.grid;
        private AStar Pathfinder => hub.pathfinder;

        public LineRenderer lineRenderer;

        private Vector2Int _lastAgent;
        private Vector2Int _lastTarget;
        private Camera _camera;

        private void Awake() {
            _camera = Camera.main;
        }
        private void Update() {
            var targetCell = Grid.PositionToCell(_camera.ScreenToWorldPoint(Input.mousePosition));
            var agentCell = Grid.PositionToCell(hub.agent.transform.position);

            if (targetCell == _lastTarget && agentCell == _lastAgent || !Grid.IsWalkable(targetCell))
                return;

            var path = Pathfinder.FindPath(agentCell, targetCell);

            if (path == null)
                return;

            UpdatePathVisualization(path);
            _lastTarget = targetCell;
            _lastAgent = agentCell;
        }

        private void UpdatePathVisualization(List<Vector3> path) {
            if (path == null || path.Count == 0) {
                lineRenderer.positionCount = 0;
                return;
            }

            path.Insert(0, hub.agent.transform.position);
            lineRenderer.positionCount = path.Count;
            lineRenderer.SetPositions(path.ToArray());
        }

        private void OnDrawGizmos() {
            if (!Application.isPlaying)
                return;

            // Визуализация узлов пути
            Gizmos.color = Color.cyan;

            if (_lastTarget == Vector2Int.zero)
                return;

            var targetNode = Grid.GetNode(_lastTarget);

            if (targetNode != null)
                Gizmos.DrawSphere(Grid.CellToPosition(targetNode.Position), 0.2f);
        }
    }
}