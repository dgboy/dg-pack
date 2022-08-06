using System;
using TMPro;
using UnityEngine;

public class WindowResizer : MonoBehaviour {
    public static event Action<int, int> OnResize;
    [SerializeField] private TMP_Dropdown windowSize;
    public static float aspectRatio;
    private ScreenOrientation _orientation;

    private void Awake() {
        _orientation = Screen.orientation;
    }

    private void Start() {
        OnChangeWindowSize();
    }

    private void Update() {
        if (_orientation != Screen.orientation) {
            _orientation = Screen.orientation;
            Debug.Log($"[detect] cur orientation: {_orientation}");
            OnResize?.Invoke(0, 0);
        }
    }

    public void OnChangeWindowSize() {
        var resolution = GetResolution();
        var baseH = GetBaseHeight();
        aspectRatio = resolution.x / (float)resolution.y;

        int w = (int)(baseH * aspectRatio);
        int h = baseH;
        Debug.Log($"[set] w: {w}, h: {h}, k: {aspectRatio}");

#if PLATFORM_STANDALONE && !UNITY_EDITOR
        OnResize?.Invoke(w, h);
        Screen.SetResolution(w, h, FullScreenMode.Windowed);
#endif
        Debug.Log($"[get] sw: {Screen.width}, sh: {Screen.height}, scrw: {Screen.currentResolution.width}");
    }

    public int GetBaseHeight() => windowSize.value switch {
        0 => 960,
        1 => 600,
        _ => 360,
    };

    public Vector2Int GetResolution() {
        string[] resolution = windowSize.options[windowSize.value].text.Split(':');
        if (resolution.Length == 2) {
            return new(int.Parse(resolution[0]), int.Parse(resolution[1]));
        }
        return Vector2Int.one;
    }
}