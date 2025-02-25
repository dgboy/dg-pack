using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class Node {
        public bool IsWalkable; // Можно ли пройти через этот узел
        public Vector2Int Position; // Позиция на сетке (x, y)
        public int GCost; // Стоимость пути от старта
        public int HCost; // Эвристическая оценка до цели
        public int FCost => GCost + HCost; // Общая стоимость
        public Node Parent; // Родительский узел для восстановления пути

        public Node(bool isWalkable, Vector2Int position) {
            IsWalkable = isWalkable;
            Position = position;
        }
    }
}