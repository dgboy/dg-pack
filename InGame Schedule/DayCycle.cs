using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace InGameSchedule {
    public class DayCycle : MonoBehaviour {
        [SerializeField] private Gradient dayLightGradient;
        private Light2D _dayLight;

        private void Awake() {
            InGameTime.OnMinuteChanged += UpdateDayLight;
            _dayLight = GetComponent<Light2D>();
        }

        private void UpdateDayLight(DateTime now) {
            float t = (now.Hour * 60 + now.Minute) / (24f * 60);
            //float t = now.Minute / 60f;
            _dayLight.color = dayLightGradient.Evaluate(t);

            // Debug.Log($"hour: {now.Hour}, t: {t}, color: {_dayLight.color}");
        }

        private void OnDisable() {
            InGameTime.OnMinuteChanged -= UpdateDayLight;
        }
    }
}