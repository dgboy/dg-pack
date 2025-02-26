using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class PathVisualizer : MonoBehaviour {
        [SerializeField] private PathfinderHub hub;
        private GridManager Grid => (GridManager)hub.grid;
        private AStar Pathfinder => hub.pathfinder;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _agent;

        private Vector2Int _lastTargetPos;

        void Update() {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            Vector2Int targetGridPos = Grid.WorldToGridPosition(mouseWorldPos);

            // Проверяем, изменилась ли позиция и доступна ли она
            if (targetGridPos != _lastTargetPos && Grid.IsPositionWalkable(targetGridPos)) {
                Vector2Int agentGridPos = Grid.WorldToGridPosition(_agent.position);
                List<Node> path = Pathfinder.FindPath(agentGridPos, targetGridPos);

                UpdatePathVisualization(path);
                _lastTargetPos = targetGridPos;
            }
        }

        void UpdatePathVisualization(List<Node> path) {
            if (path == null || path.Count == 0) {
                _lineRenderer.positionCount = 0;
                return;
            }

            // Добавляем начальную позицию агента
            Vector3[] positions = new Vector3[path.Count + 1];
            positions[0] = _agent.position;

            for (int i = 0; i < path.Count; i++) {
                positions[i + 1] = Grid.GridToWorldPosition(path[i].Position);
            }

            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
        }

        void OnDrawGizmos() {
            if (!Application.isPlaying)
                return;

            // Визуализация узлов пути
            Gizmos.color = Color.cyan;

            if (_lastTargetPos != Vector2Int.zero) {
                Node targetNode = Grid.GetNode(_lastTargetPos);

                if (targetNode != null) {
                    Gizmos.DrawSphere(Grid.GridToWorldPosition(targetNode.Position), 0.2f);
                }
            }
        }
    }
}