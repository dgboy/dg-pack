using System.Collections.Generic;
using DG_Pack.Base;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class AStar {
        public AStar(IGridManager grid) => Grid = grid;

        private IGridManager Grid { get; }


        public List<Vector3> FindPath(Vector3 from, Vector3 to) {
            var targetCell = Grid.PositionToCell(to);

            // Проверяем, изменилась ли позиция и доступна ли она
            if (from == to || !Grid.IsWalkable(targetCell))
                return null;

            var currentCell = Grid.PositionToCell(from);
            return FindPath(currentCell, targetCell);
        }

        public List<Vector3> FindPath(Vector2Int from, Vector2Int to) {
            var startNode = Grid.GetNode(from);
            var targetNode = Grid.GetNode(to);

            var openSet = new List<Node>();
            var closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                // Находим узел с наименьшей FCost
                var currentNode = FindCheapNode(openSet, openSet[0]);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // Путь найден
                if (currentNode == targetNode)
                    return RetracePath(startNode, targetNode);

                // Получаем соседние узлы
                foreach (var neighbour in GetNeighbours(currentNode)) {
                    if (neighbour is { IsWalkable: true } && !closedSet.Contains(neighbour))
                        TryCostNeighbourNode(neighbour, currentNode, openSet, targetNode);
                }
            }

            return null; // Путь не найден
        }

        private static Node FindCheapNode(List<Node> set, Node currentNode) {
            for (int i = 1; i < set.Count; i++) {
                if (
                    set[i].FCost < currentNode.FCost ||
                    set[i].FCost == currentNode.FCost && set[i].HCost < currentNode.HCost
                ) {
                    currentNode = set[i];
                }
            }

            return currentNode;
        }
        private List<Node> GetNeighbours(Node node) {
            var neighbours = new List<Node>();

            foreach (var dir in VectorEx.Direction4D) {
                int x = node.Position.x + dir.x;
                int y = node.Position.y + dir.y;
                var offset = Vector2Int.zero; //-Grid.Size / 2;


                if (x >= offset.x && x < Grid.SizeX && y >= offset.y && y < Grid.SizeY)
                    neighbours.Add(Grid.GetNode(new Vector2Int(x, y)));
            }

            return neighbours;
        }
        private static void TryCostNeighbourNode(Node neighbour, Node current, List<Node> openSet, Node targetNode) {
            int newMovementCostToNeighbour = current.GCost + GetDistance(current, neighbour);

            if (newMovementCostToNeighbour >= neighbour.GCost && openSet.Contains(neighbour))
                return;

            neighbour.GCost = newMovementCostToNeighbour;
            neighbour.HCost = GetDistance(neighbour, targetNode);
            neighbour.Parent = current;

            if (!openSet.Contains(neighbour))
                openSet.Add(neighbour);
        }


        private List<Vector3> RetracePath(Node startNode, Node endNode) {
            var path = new List<Vector3>();
            var currentNode = endNode;

            while (currentNode != startNode) {
                path.Add(Grid.CellToPosition(currentNode.Position));
                currentNode = currentNode.Parent;
            }

            path.Reverse();
            return path;
        }
        private static int GetDistance(Node a, Node b) {
            // Манхэттенское расстояние
            int dstX = Mathf.Abs(a.Position.x - b.Position.x);
            int dstY = Mathf.Abs(a.Position.y - b.Position.y);
            return dstX + dstY;
        }
    }
}