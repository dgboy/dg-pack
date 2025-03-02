using UnityEngine;

namespace DG_Pack.Pathfinding {
    public abstract class GridManagerBase : MonoBehaviour, IGridManager {
        public abstract int SizeX { get; }
        public abstract int SizeY { get; }
        public abstract Vector2Int Size { get; }


        public abstract void Initialize();
        public abstract Node GetNode(Vector2Int cell);
        public abstract Vector2Int WorldToGridPosition(Vector3 worldPos);
        public abstract bool IsPositionWalkable(Vector2Int cell);
        public abstract Vector3 GridToWorldPosition(Vector2Int cell);
    }
}