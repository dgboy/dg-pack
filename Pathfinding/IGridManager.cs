using UnityEngine;

namespace DG_Pack.Pathfinding {
    public interface IGridManager {
        int SizeX { get; }
        int SizeY { get; }
        Vector2Int Size { get; }

        Node GetNode(Vector2Int cell);
    }
}