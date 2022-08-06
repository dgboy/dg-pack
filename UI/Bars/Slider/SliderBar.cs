using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour {
    public Slider slider;
    [SerializeField] private FloatValue data = null;

    private void Start() {
        slider.value = data.value;
    }

    public void Fresh() {
        slider.value = data.value;
    }
}
