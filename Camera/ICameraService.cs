using UnityEngine;

namespace DG_Pack.Camera {
    public interface ICameraService {
        Transform Target { get; set; }
    }
}