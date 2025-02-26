using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class GridManager : MonoBehaviour {
        public int GridSizeX = 10;
        public int GridSizeY = 10;
        public float ObstacleChance = 0.2f;

        private Node[,] grid;

        public Node[,] Grid => grid;
        public int MaxGridX => GridSizeX - 1;
        public int MaxGridY => GridSizeY - 1;

        void Start() {
            InitializeGrid();
        }

        public void InitializeGrid() {
            grid = new Node[GridSizeX, GridSizeY];

            for (int x = 0; x < GridSizeX; x++) {
                for (int y = 0; y < GridSizeY; y++) {
                    bool walkable = Random.Range(0f, 1f) > ObstacleChance;
                    grid[x, y] = new Node(walkable, new Vector2Int(x, y));
                }
            }
        }

        public Node GetNodeAtPosition(Vector2Int gridPosition) {
            if (IsPositionValid(gridPosition))
                return grid[gridPosition.x, gridPosition.y];

            return null;
        }

        public bool IsPositionValid(Vector2Int gridPosition) {
            return gridPosition.x >= 0 && gridPosition.x < GridSizeX &&
                   gridPosition.y >= 0 && gridPosition.y < GridSizeY;
        }

        public bool IsPositionWalkable(Vector2Int gridPosition) {
            return IsPositionValid(gridPosition) && grid[gridPosition.x, gridPosition.y].IsWalkable;
        }

        public Vector2Int WorldToGridPosition(Vector3 worldPos) {
            return new Vector2Int(
                Mathf.RoundToInt(worldPos.x),
                Mathf.RoundToInt(worldPos.y)
            );
        }

        public Vector3 GridToWorldPosition(Vector2Int gridPos) {
            return new Vector3(gridPos.x, gridPos.y, 0);
        }

        void OnDrawGizmos() {
            if (grid == null) return;

            foreach (Node node in grid) {
                Gizmos.color = node.IsWalkable ? Color.white : Color.red;
                Vector3 pos = GridToWorldPosition(node.Position);

                if (node.IsWalkable)
                    Gizmos.DrawCube(pos, Vector3.one * 0.9f);
                else
                    Gizmos.DrawWireCube(pos, Vector3.one * 0.9f);
            }
        }
    }
}