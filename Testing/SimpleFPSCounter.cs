using UnityEngine;

namespace DG_Pack.Testing {
    [ExecuteInEditMode]
    public class SimpleFPSCounter : MonoBehaviour {
        [Header("Settings")]
        [SerializeField] private int fontSize = 16;
        [SerializeField] private Color color = Color.red;
        [SerializeField] private Rect rect = new(10, 10, 100, 20);
        [SerializeField] private GUIStyle style = new();
        private float _fps = 60f;
        private float _avg = 0f;


        private void Awake() {
            enabled = Debug.isDebugBuild;
        }

        private void Update() {
            _avg += (Time.deltaTime / Time.timeScale - _avg) * 0.03f;
            _fps = 1f / _avg;
        }

        private void OnGUI() {
            style.normal.textColor = color;
            style.fontSize = fontSize;
            if (enabled) GUI.Label(rect, "FPS: " + _fps, style);
        }
    }
}