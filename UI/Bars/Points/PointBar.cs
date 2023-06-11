using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointBar : MonoBehaviour {
    [SerializeField] private Image heart;
    [SerializeField] private int unitFactor = 2;
    [SerializeField] private Sprite[] states;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    
    private readonly List<Image> _items = new List<Image>();


    public void Init(int max, int value) {
        _items.AddRange(GetComponentsInChildren<Image>());
        // transform.Clear();
        Refresh(max, value);
    }
    
    public void Refresh(int max, float value) {
        int maxCount = max / 2;
        Debug.Log($"health: {value} / {max}");

        if (maxCount > _items.Count) Create();

        for (int i = 0; i < maxCount; i++) {
            var image = _items[i].GetComponent<Image>();

            if (i + 1 <= value) {
                image.sprite = fullHeart;
            } else if (i >= value) {
                image.sprite = emptyHeart;
            } else {
                image.sprite = halfHeart;
            }
        }
    }


    private void Create() {
        var newPoint = Instantiate(heart, transform);
        newPoint.name = $"point {transform.childCount}";
        _items.Add(newPoint);
    }
}