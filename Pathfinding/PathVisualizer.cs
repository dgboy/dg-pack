using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class PathVisualizer : MonoBehaviour {
        public PathfinderHub hub; // Ссылка на компонент A*

        public LineRenderer lineRenderer; // Компонент для отрисовки линии
        public Transform agent; // Объект, который двигается по пути

        private Vector2Int LastCell { get; set; }

        private void Update() {
            // 1. Получаем позицию мыши в мировых координатах
            var mouseWorldPos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            // 2. Конвертируем в координаты сетки
            var targetGridPos = WorldToGridPosition(mouseWorldPos);

            // 3. Если позиция мыши изменилась и узел проходимый
            if (targetGridPos != LastCell && IsPositionWalkable(targetGridPos)) {
                // 4. Находим путь от текущей позиции агента к мыши
                var agentGridPos = WorldToGridPosition(agent.position);
                var path = hub.pathfinder.FindPath(agentGridPos, targetGridPos);

                // 5. Обновляем визуализацию
                UpdatePathVisualization(path);
                LastCell = targetGridPos;
            }
        }

        // Предполагаем, что сетка начинается в (0,0) и каждая ячейка = 1 юниту
        private static Vector2Int WorldToGridPosition(Vector3 worldPos) =>
            new(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));

        private bool IsPositionWalkable(Vector2Int cell) {
            if (cell.x < 0 || cell.x >= hub.grid.SizeX || cell.y < 0 || cell.y >= hub.grid.SizeY)
                return false;

            var node = hub.grid.GetNode(cell);
            return node is { IsWalkable: true };
        }

        private void UpdatePathVisualization(List<Node> path) {
            if (path == null || path.Count == 0) {
                lineRenderer.positionCount = 0;
                return;
            }

            // Конвертируем узлы в мировые координаты
            var positions = new Vector3[path.Count + 1];
            positions[0] = agent.position; // Начальная позиция агента

            for (int i = 0; i < path.Count; i++)
                positions[i + 1] = new Vector3(path[i].Position.x, path[i].Position.y, 0);

            // Настраиваем LineRenderer
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }

        // Для отладки: рисуем Gizmos целевой позиции
        private void OnDrawGizmos() {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(new Vector3(LastCell.x, LastCell.y, 0), 0.3f);
        }
    }
}