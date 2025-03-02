using UnityEngine;

namespace DG_Pack.Pathfinding {
    public interface IGridManager {
        int SizeX { get; }
        int SizeY { get; }
        Vector2Int Size { get; }

        Node GetNode(Vector2Int cell);
        Vector2Int PositionToCell(Vector3 position);
        bool IsWalkable(Vector2Int cell);
        Vector3 CellToPosition(Vector2Int cell);
    }
}