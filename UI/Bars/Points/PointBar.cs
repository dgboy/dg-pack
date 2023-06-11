using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointBar : MonoBehaviour {
    [SerializeField] private Image heart;
    [SerializeField] private Sprite[] states;

    private readonly List<Image> _items = new List<Image>();
    private int StateFactor => states.Length - 1;


    public void Init(int max, int value) {
        _items.AddRange(GetComponentsInChildren<Image>());
        // transform.Clear();
        Refresh(max, value);
        // Debug.Log($"[{gameObject.name}] health: {value} / {max}");
    }

    public void Refresh(int max, int value) {
        if (max / StateFactor > _items.Count) Create();
        else Clear(max);

        Fill(value);
    }
    private void Fill(int value) {
        for (int i = 0; i < _items.Count; i++) {
            var image = _items[i].GetComponent<Image>();
            image.sprite = states[Mathf.Clamp(value - i * StateFactor, 0, StateFactor)];
        }
    }


    private void Create() {
        var newPoint = Instantiate(heart, transform);
        newPoint.name = $"point {transform.childCount}";
        _items.Add(newPoint);
    }
    
    private void Clear(int max) {
        for (int i = _items.Count; i > 0; i--) {
            if (i > max / StateFactor)
                Destroy(_items[i]);
        }
    }
}