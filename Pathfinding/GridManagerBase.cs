using UnityEngine;

namespace DG_Pack.Pathfinding {
    public abstract class GridManagerBase : MonoBehaviour, IGridManager {
        public abstract int SizeX { get; }
        public abstract int SizeY { get; }
        public abstract Vector2Int Size { get; }


        public abstract void Initialize();

        public abstract Node GetNode(Vector2Int cell);
        public abstract bool IsWalkable(Vector2Int cell);
        public abstract bool IsWalkable(Vector3 position);

        public abstract Vector2Int PositionToCell(Vector3 position);
        public abstract Vector3 CellToPosition(Vector2Int cell);
    }
}