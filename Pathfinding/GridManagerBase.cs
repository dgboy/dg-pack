using UnityEngine;

namespace DG_Pack.Pathfinding {
    public abstract class GridManagerBase : MonoBehaviour, IGridManager {
        public abstract int SizeX { get; }
        public abstract int SizeY { get; }
        public abstract Vector2Int Size { get; }
        public abstract Node GetNode(Vector2Int cell);
        
    }
}