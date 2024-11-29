using UnityEngine;

namespace DG_Pack.Testing {
    public class Stopwatch : MonoBehaviour {
        public bool ShowGUI = true;
        private static bool counting;
        private static float timer = 0;

        void Update() {
        
            if (counting) {
                timer += 1 * Time.unscaledDeltaTime;
            }
        }
        public static void Begin() {
            timer = 0;
            counting = true;
        }
        public static void Stop() {
            counting = false;
        }

        private void OnGUI() {
            if (ShowGUI) GUI.Label(new Rect(30, 30, 230, 130), "time: " + timer.ToString("f2"));
        }
    }
}
