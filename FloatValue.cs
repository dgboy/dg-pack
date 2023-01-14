using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Values/Float", fileName = "Float Value")]
public class FloatValue : ScriptableObject {
    [SerializeField] private float defaultValue = 1;
    public float value;

    private void OnEnable() {
        value = defaultValue;
    }
}
