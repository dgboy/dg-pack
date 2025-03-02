using DG_Pack.Base;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG_Pack.Pathfinding {
    [RequireComponent(typeof(Grid))]
    public class GridManager2 : GridManagerBase {
        [SerializeField] private Tilemap obstacleTilemap;
        [SerializeField] private CompositeCollider2D levelCollider;
        [SerializeField] private LayerMask obstacleLayer;

        private Grid _unityGrid;
        private BoundsInt _bounds;
        private Node[,] Nodes { get; set; }

        public override Vector2Int Size => new(_bounds.size.x, _bounds.size.y);
        public override int SizeX => _bounds.size.x;
        public override int SizeY => _bounds.size.y;



        public override void Initialize() {
            _unityGrid = GetComponent<Grid>();
            _bounds = obstacleTilemap.cellBounds;

            Nodes = new Node[_bounds.size.x, _bounds.size.y];

            // 3. Заполняем сетку данными
            for (int x = 0; x < _bounds.size.x; x++) {
                for (int y = 0; y < _bounds.size.y; y++) {
                    var cellPosition = new Vector3Int(_bounds.x + x, _bounds.y + y, _bounds.z);

                    // 4. Проверяем коллайдеры в ячейке
                    var shift = Vector3.one * 0.1f; // коллайдер слегка задевает проходимые узлы тоже
                    var cellToWorld = _unityGrid.CellToWorld(cellPosition) + shift;
                    bool hasCollider = CheckCollisionAtPosition(cellToWorld);
                    // 5. Проверяем наличие тайла
                    bool hasTile = obstacleTilemap.HasTile(cellPosition);

                    Nodes[x, y] = new Node(new Vector2Int(x, y), !hasCollider && !hasTile);
                }
            }
        }

        private bool CheckCollisionAtPosition(Vector3 worldPos) {
            // Проверяем коллайдеры в радиусе 0.1 единицы
            var hit = Physics2D.OverlapCircle(worldPos, 0.01f, obstacleLayer);
            return hit != null && hit.gameObject.CompareTag("Walls");
        }

        public override Node GetNode(Vector2Int cell) {
            if (!IsPositionValid(cell))
                return null;

            int x = cell.x;
            int y = cell.y;

            if (x < 0 || x >= Nodes.GetLength(0) || y < 0 || y >= Nodes.GetLength(1))
                return null;

            return Nodes[x, y];
        }

        public override bool IsPositionWalkable(Vector2Int cell) =>
            IsPositionValid(cell) && Nodes[cell.x, cell.y].IsWalkable;
        public override Vector2Int WorldToGridPosition(Vector3 worldPos) {
            var cell = _unityGrid.WorldToCell(worldPos);
            return new Vector2Int(cell.x - _bounds.x, cell.y - _bounds.y);
        }
        public override Vector3 GridToWorldPosition(Vector2Int cell) {
            var cellPosition = new Vector3Int(_bounds.x + cell.x, _bounds.y + cell.y, _bounds.z);
            return _unityGrid.CellToWorld(cellPosition) + _unityGrid.cellSize / 2f;
        }

        private bool IsPositionValid(Vector2Int cell) =>
            cell.x >= 0 && cell.x < _bounds.size.x &&
            cell.y >= 0 && cell.y < _bounds.size.y;


        private void OnDrawGizmos() {
            if (Nodes == null) return;

            const float scale = 0.9f;

            foreach (var node in Nodes) {
                Gizmos.color = (node.IsWalkable ? Color.white : Color.red).Alpha(0.5f);
                var pos = GridToWorldPosition(node.Position);


                if (node.IsWalkable)
                    Gizmos.DrawCube(pos, Vector3.one * scale);
                else
                    Gizmos.DrawWireCube(pos, Vector3.one * scale);
            }
        }
    }
}