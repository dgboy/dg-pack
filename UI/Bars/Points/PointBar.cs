using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointBar : MonoBehaviour {
    [SerializeField] private Transform content;
    [SerializeField] private GameObject heart;
    [SerializeField] private Points points;
    [SerializeField] private int unitFactor = 2;
    [SerializeField] private Sprite[] states;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    private readonly List<GameObject> pointItems = new();

    public void Init() {
        content.Clear();

        for (int i = 0; i < points.Max / unitFactor; i++) {
            Create();
        }
    }

    public void Create() {
        GameObject newPoint = Instantiate(heart, content);
        newPoint.name = $"point {content.childCount}";
        pointItems.Add(newPoint);
    }

    public void Fresh(float current) {
        int max = points.Max / 2;
        //float current = (float)points.Current / 2;

        Debug.Log($"health: {current} / {max}"); ;
        if (max > pointItems.Count) {
            Create();
        }

        for (int i = 0; i < max; i++) {
            Image image = pointItems[i].GetComponent<Image>();

            if (i + 1 <= current) {
                image.sprite = fullHeart;
            } else if (i >= current) {
                image.sprite = emptyHeart;
            } else {
                image.sprite = halfHeart;
            }
        }
    }
}
