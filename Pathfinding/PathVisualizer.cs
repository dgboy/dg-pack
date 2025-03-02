using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class PathVisualizer : MonoBehaviour {
        [SerializeField] private PathfinderHub hub;
        private GridManagerBase Grid => hub.grid;
        private AStar Pathfinder => hub.pathfinder;

        public LineRenderer lineRenderer;

        private Vector2Int _lastTargetPos;
        private Camera _camera;

        private void Awake() {
            _camera = Camera.main;
        }
        private void Update() {
            var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var targetGridPos = Grid.WorldToGridPosition(mouseWorldPos);

            // Проверяем, изменилась ли позиция и доступна ли она
            if (targetGridPos == _lastTargetPos || !Grid.IsPositionWalkable(targetGridPos))
                return;

            var agentGridPos = Grid.WorldToGridPosition(hub.agent.position);
            var path = Pathfinder.FindPath(agentGridPos, targetGridPos);
            UpdatePathVisualization(path);

            _lastTargetPos = targetGridPos;
        }

        private void UpdatePathVisualization(List<Node> path) {
            if (path == null || path.Count == 0) {
                lineRenderer.positionCount = 0;
                return;
            }

            // Добавляем начальную позицию агента
            var positions = new Vector3[path.Count + 1];
            positions[0] = hub.agent.position;

            for (int i = 0; i < path.Count; i++)
                positions[i + 1] = Grid.GridToWorldPosition(path[i].Position);

            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }

        private void OnDrawGizmos() {
            if (!Application.isPlaying)
                return;

            // Визуализация узлов пути
            Gizmos.color = Color.cyan;

            if (_lastTargetPos == Vector2Int.zero)
                return;

            var targetNode = Grid.GetNode(_lastTargetPos);

            if (targetNode != null)
                Gizmos.DrawSphere(Grid.GridToWorldPosition(targetNode.Position), 0.2f);
        }
    }
}