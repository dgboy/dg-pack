using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Values/Points", fileName = "Points")]
public class Points : ScriptableObject {
    [SerializeField] private int init;
    private int max;
    private int value;

    private void OnEnable() {
        value = max = init;
    }

    public int Max { get => max; }
    public int Current { get => value; }
    public bool Has => value > 0;

    public void Set(int amount) => value = amount;
    public void RaiseMax(int amount = 2) => value = max += amount;
    
    public void ToZero() => value = 0;
    public void ToMax() => value = Max;

    public virtual void Increase(int amount) {
        value = (value + amount > Max) ? Max : value + amount;
    }
    public virtual void Decrease(int amount) {
        value = (value - amount <= 0) ? 0 : value - amount;
    }
}
