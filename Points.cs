using System;
using UnityEngine;

[Serializable]
public class Points {
    public int max;
    public int value;

    public Points(int max) {
        value = this.max = max;
    }


    public void Set(int amount) => value = amount;
    public void RaiseMax(int amount = 2) => value = max += amount;

    public void ToZero() => value = 0;
    public void ToMax() => value = max;

    public void Increase(int amount) {
        value = Mathf.Clamp(value + amount, 0, max);
    }
    public void Decrease(int amount) {
        value = Mathf.Clamp(value - amount, 0, max);
    }
}