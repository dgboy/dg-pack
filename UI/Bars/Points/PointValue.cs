using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Values/Points", fileName = "Points 2")]
public class PointValue : ScriptableObject {
    [SerializeField] private float init = 1;
    public float max;
    public float current;
    public float factor = 2;

    private void OnEnable() {
        current = init;
    }
}
