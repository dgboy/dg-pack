using DG_Pack.Base;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class GridManager : GridManagerBase {
        [SerializeField] private Vector2Int size = 10 * Vector2Int.one;

        public override Vector2Int Size => size;
        public override int SizeX => size.x;
        public override int SizeY => size.y;

        public float obstacleChance = 0.2f;

        private Node[,] Nodes { get; set; }


        private void Start() {
            InitializeGrid();
        }

        public void InitializeGrid() {
            var pivot = Size * Vector2Int.one / 2;
            Nodes = new Node[SizeX, SizeY];

            for (int x = 0; x < SizeX; x++) {
                for (int y = 0; y < SizeY; y++) {
                    bool walkable = Random.Range(0f, 1f) > obstacleChance;
                    Nodes[x, y] = new Node(walkable, new Vector2Int(x, y) - pivot);
                }
            }
        }

        public override Node GetNode(Vector2Int cell) => IsPositionValid(cell) ? Nodes[cell.x, cell.y] : null;


        public bool IsPositionWalkable(Vector2Int gridPosition) =>
            IsPositionValid(gridPosition) && Nodes[gridPosition.x, gridPosition.y].IsWalkable;

        public Vector2Int WorldToGridPosition(Vector3 worldPos) =>
            new(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));

        private bool IsPositionValid(Vector2Int cell) =>
            cell.x >= 0 && cell.x < SizeX &&
            cell.y >= 0 && cell.y < SizeY;

        public Vector3 GridToWorldPosition(Vector2Int gridPos) => new(gridPos.x, gridPos.y, 0);

        private void OnDrawGizmos() {
            if (Nodes == null) return;

            foreach (var node in Nodes) {
                Gizmos.color = (node.IsWalkable ? Color.white : Color.red).Alpha(0.5f);
                var pos = GridToWorldPosition(node.Position);

                if (node.IsWalkable)
                    Gizmos.DrawCube(pos, Vector3.one * 0.9f);
                else
                    Gizmos.DrawWireCube(pos, Vector3.one * 0.9f);
            }
        }
    }
}