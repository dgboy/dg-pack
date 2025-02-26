using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class PathVisualizer : MonoBehaviour {
        public AStar pathfinder; // Ссылка на компонент A*
        public LineRenderer lineRenderer; // Компонент для отрисовки линии
        public Transform agent; // Объект, который двигается по пути
        public LayerMask gridLayer; // Слой, на котором находится сетка

        private Vector2Int lastTargetPos;

        void Update() {
            // 1. Получаем позицию мыши в мировых координатах
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            // 2. Конвертируем в координаты сетки
            Vector2Int targetGridPos = WorldToGridPosition(mouseWorldPos);

            // 3. Если позиция мыши изменилась и узел проходимый
            if (targetGridPos != lastTargetPos && IsPositionWalkable(targetGridPos)) {
                // 4. Находим путь от текущей позиции агента к мыши
                Vector2Int agentGridPos = WorldToGridPosition(agent.position);
                List<Node> path = pathfinder.FindPath(agentGridPos, targetGridPos);

                // 5. Обновляем визуализацию
                UpdatePathVisualization(path);
                lastTargetPos = targetGridPos;
            }
        }

        Vector2Int WorldToGridPosition(Vector3 worldPos) {
            // Предполагаем, что сетка начинается в (0,0) и каждая ячейка = 1 юниту
            return new Vector2Int(
                Mathf.RoundToInt(worldPos.x),
                Mathf.RoundToInt(worldPos.y)
            );
        }

        bool IsPositionWalkable(Vector2Int gridPos) {
            // Проверка выхода за границы и проходимости
            if (gridPos.x >= 0 && gridPos.x < pathfinder.gridSizeX &&
                gridPos.y >= 0 && gridPos.y < pathfinder.gridSizeY) {
                return pathfinder.grid[gridPos.x, gridPos.y].IsWalkable;
            }

            return false;
        }

        void UpdatePathVisualization(List<Node> path) {
            if (path == null || path.Count == 0) {
                lineRenderer.positionCount = 0;
                return;
            }

            // Конвертируем узлы в мировые координаты
            Vector3[] positions = new Vector3[path.Count + 1];
            positions[0] = agent.position; // Начальная позиция агента

            for (int i = 0; i < path.Count; i++) {
                positions[i + 1] = new Vector3(
                    path[i].Position.x,
                    path[i].Position.y,
                    0
                );
            }

            // Настраиваем LineRenderer
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }

        // Для отладки: рисуем Gizmos целевой позиции
        void OnDrawGizmos() {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(new Vector3(lastTargetPos.x, lastTargetPos.y, 0), 0.3f);
        }
    }
}