using UnityEngine;

namespace DG_Pack.Pathfinding {
    [System.Serializable]
    public class Node {
        public bool walkable; // Можно ли пройти через этот узел
        
        public Vector2Int position; // Позиция на сетке (x, y)
        public Node parent; // Родительский узел для восстановления пути

        public int gCost; // Стоимость пути от старта
        public int hCost; // Эвристическая оценка до цели
        public int FCost => gCost + hCost; // Общая стоимость


        public Node(Vector2Int position, bool walkable) {
            this.walkable = walkable;
            this.position = position;
        }
    }
}