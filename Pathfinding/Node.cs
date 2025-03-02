using UnityEngine;

namespace DG_Pack.Pathfinding {
    [System.Serializable]
    public class Node {
        public bool IsWalkable; // Можно ли пройти через этот узел
        public Vector2Int Position; // Позиция на сетке (x, y)
        public int GCost; // Стоимость пути от старта
        public int HCost; // Эвристическая оценка до цели
        public int FCost => GCost + HCost; // Общая стоимость
        public Node Parent; // Родительский узел для восстановления пути

        public Node(Vector2Int position, bool isWalkable) {
            IsWalkable = isWalkable;
            Position = position;
        }
    }
}