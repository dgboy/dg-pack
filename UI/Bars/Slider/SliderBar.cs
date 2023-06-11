using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour {
    public Slider slider;

    public void Refresh(float value, float max) {
        slider.value = value;
        slider.maxValue = max;
    }
}