using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {
    private float fps = 60f;
    float avg = 0f;

    [Header("Display On GUI")]
    public bool ShowGUI = true;
    public GUIStyle style = new();
    public Rect rect = new Rect(10, 10, 100, 20);

    [Header("Display In Text UI Element")]
    public Text FPSLabel;
    //[Header("Customize")]

    void Update() {
        avg += ((Time.deltaTime / Time.timeScale) - avg) * 0.03f; //run this every frame
        fps = 1f / avg;
        //FPS = 1 / Time.unscaledDeltaTime;

        if (FPSLabel) FPSLabel.text = "FPS: " + fps;
    }

    private void OnGUI() {
        if (ShowGUI) GUI.Label(rect, "FPS: " + fps.ToString(), style);
    }


    //public float updateInterval = 0.5F;
    //private double lastInterval;
    //private int frames = 0;
    //private float fps;
    //void Start() {
    //    lastInterval = Time.realtimeSinceStartup;
    //    frames = 0;
    //}

    //void OnGUI() {
    //    GUILayout.Label("" + fps.ToString("f2"));
    //}

    //void Update() {
    //    ++frames;
    //    float timeNow = Time.realtimeSinceStartup;
    //    if (timeNow > lastInterval + updateInterval) {
    //        fps = (float)(frames / (timeNow - lastInterval));
    //        frames = 0;
    //        lastInterval = timeNow;
    //    }
    //}
}
