using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class AStar {
        public AStar(IGridManager grid) => Grid = grid;

        public IGridManager Grid { get; }


        public List<Node> FindPath(Vector2Int from, Vector2Int to) {
            var startNode = Grid.GetNode(from);
            var targetNode = Grid.GetNode(to);

            var openSet = new List<Node>();
            var closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                var currentNode = openSet[0];

                // Находим узел с наименьшей FCost
                for (int i = 1; i < openSet.Count; i++) {
                    if (
                        openSet[i].FCost < currentNode.FCost ||
                        openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost
                    ) {
                        currentNode = openSet[i];
                    }
                }

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

        private List<Node> GetNeighbours(Node node) {
            var neighbours = new List<Node>();

            // 4-сторонние соседи (можно добавить диагонали)
            Vector2Int[] directions = {
                new(0, 1),
                new(1, 0),
                new(0, -1),
                new(-1, 0),
            };

            foreach (var dir in directions) {
                int x = node.Position.x + dir.x;
                int y = node.Position.y + dir.y;

                if (x >= 0 && x < Grid.SizeX && y >= 0 && y < Grid.SizeY)
                    neighbours.Add(Grid.GetNode(new Vector2Int(x, y)));
            }

            return neighbours;
        }


        private static List<Node> RetracePath(Node startNode, Node endNode) {
            var path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode) {
                path.Add(currentNode);
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