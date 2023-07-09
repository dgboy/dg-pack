using UnityEngine;

public interface IMoveableMap {
    public Vector3 GetCoordinates(Vector2 position);
    public bool IsPassablePath(Vector3 position);
}
