using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class AStar : MonoBehaviour {
        public int gridSizeX = 10;
        public int gridSizeY = 10;

        private Node[,] grid;


        private void Start() {
            // Инициализация тестовой сетки (0,0 - старт, 9,9 - цель)
            InitializeGrid();
            var path = FindPath(new Vector2Int(0, 0), new Vector2Int(9, 9));

            // Визуализация пути
            if (path != null) {
                foreach (var node in path) {
                    Debug.Log(node.Position);
                }
            }
        }

        private void InitializeGrid() {
            grid = new Node[gridSizeX, gridSizeY];

            // Заполняем сетку (пример: случайные препятствия)
            for (int x = 0; x < gridSizeX; x++) {
                for (int y = 0; y < gridSizeY; y++) {
                    bool walkable = Random.Range(0, 5) != 0; // 20% шанс препятствия
                    grid[x, y] = new Node(walkable, new Vector2Int(x, y));
                }
            }
        }

        public List<Node> FindPath(Vector2Int startPos, Vector2Int targetPos) {
            var startNode = grid[startPos.x, startPos.y];
            var targetNode = grid[targetPos.x, targetPos.y];

            var openSet = new List<Node>();
            var closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                var currentNode = openSet[0];

                // Находим узел с наименьшей FCost
                for (int i = 1; i < openSet.Count; i++) {
                    if (openSet[i].FCost < currentNode.FCost ||
                        (openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)) {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // Путь найден
                if (currentNode == targetNode) {
                    return RetracePath(startNode, targetNode);
                }

                // Получаем соседние узлы
                foreach (var neighbour in GetNeighbours(currentNode)) {
                    if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                        continue;

                    int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);

                    if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour)) {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            return null; // Путь не найден
        }

        private List<Node> RetracePath(Node startNode, Node endNode) {
            var path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode) {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();
            return path;
        }

        private int GetDistance(Node a, Node b) {
            // Манхэттенское расстояние
            int dstX = Mathf.Abs(a.Position.x - b.Position.x);
            int dstY = Mathf.Abs(a.Position.y - b.Position.y);
            return dstX + dstY;
        }

        private List<Node> GetNeighbours(Node node) {
            var neighbours = new List<Node>();

            // 4-сторонние соседи (можно добавить диагонали)
            Vector2Int[] directions = {
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
                new Vector2Int(0, -1),
                new Vector2Int(-1, 0)
            };

            foreach (var dir in directions) {
                int checkX = node.Position.x + dir.x;
                int checkY = node.Position.y + dir.y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }

            return neighbours;
        }

        private void OnDrawGizmos() {
            if (grid == null) return;

            foreach (var node in grid) {
                Gizmos.color = node.IsWalkable ? Color.white : Color.red;

                if (node.IsWalkable)
                    Gizmos.DrawCube(new Vector3(node.Position.x, node.Position.y, 0), Vector3.one * 0.9f);
                else
                    Gizmos.DrawWireCube(new Vector3(node.Position.x, node.Position.y, 0), Vector3.one * 0.9f);
            }
        }
    }
}