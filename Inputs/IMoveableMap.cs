using UnityEngine;

namespace DG_Pack.Inputs {
    public interface IMoveableMap {
        public Vector3 GetCoordinates(Vector2 position);
        public bool IsPassablePath(Vector3 position);
    }
}
