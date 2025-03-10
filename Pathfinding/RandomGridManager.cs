using DG_Pack.Base;
using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class RandomGridManager : GridManagerBase {
        [SerializeField] private Vector2Int size = 10 * Vector2Int.one;

        public override Vector2Int Size => size;
        public override int SizeX => size.x;
        public override int SizeY => size.y;

        public float obstacleChance = 0.2f;


        public override void Initialize() {
            var pivot = Vector2Int.zero; //Size * Vector2Int.one / 2;
            Nodes = new Node[SizeX, SizeY];

            for (int x = 0; x < SizeX; x++) {
                for (int y = 0; y < SizeY; y++) {
                    bool walkable = Random.Range(0f, 1f) > obstacleChance;
                    Nodes[x, y] = new Node(new Vector2Int(x, y) - pivot, walkable);
                }
            }
        }

        public override Node GetNode(Vector2Int cell) => IsPositionValid(cell) ? Nodes[cell.x, cell.y] : null;


        public override bool IsWalkable(Vector2Int cell) =>
            IsPositionValid(cell) && Nodes[cell.x, cell.y].walkable;
        public override bool IsWalkable(Vector3 position) {
            var cell = PositionToCell(position);
            return IsPositionValid(cell) && Nodes[cell.x, cell.y].walkable;
        }

        public override Vector2Int PositionToCell(Vector3 position) =>
            new(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));

        private bool IsPositionValid(Vector2Int cell) =>
            cell.x >= 0 && cell.x < SizeX &&
            cell.y >= 0 && cell.y < SizeY;

        public override Vector3 CellToPosition(Vector2Int cell) => new(cell.x, cell.y, 0);

        private void OnDrawGizmos() {
            if (Nodes == null) return;

            foreach (var node in Nodes) {
                Gizmos.color = (node.walkable ? Color.white : Color.red).Alpha(0.5f);
                var pos = CellToPosition(node.position);

                if (node.walkable)
                    Gizmos.DrawCube(pos, Vector3.one * 0.9f);
                else
                    Gizmos.DrawWireCube(pos, Vector3.one * 0.9f);
            }
        }
    }
}