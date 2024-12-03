using DG_Pack.Base;
using UnityEngine;

namespace DG_Pack.Services.Cam {
    public static class CameraService {
        private static Camera Main => Camera.main;


        public static Vector3 ToWorldPoint(Vector3 mousePosition) =>
            Main!.ScreenToWorldPoint(mousePosition.To3(-Main.transform.position.z));
    }
}