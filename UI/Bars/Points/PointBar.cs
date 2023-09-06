using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DG_Pack.UI.Bars.Points {
    public class PointBar : MonoBehaviour {
        [SerializeField] private Image heart;
        [SerializeField] private Sprite[] states;

        private IPointFactory Factory { get; set; }
        private List<Image> Items { get; } = new();
        private int StateFactor => states.Length - 1;

        private void Awake() =>
            Factory = new HealthPointFactory(heart, transform);
        public void Init(DG_Pack.Points health) => Init(health.max, health.value);
        public void Init(int max, int value) => Refresh(max, value);

        public void Refresh(int max, int value) {
            if (Items.Count < max / StateFactor) {
                for (int i = 0; i < max / StateFactor; i++)
                    Items.Add(Factory.Create());
            } else Clear(max);

            Fill(value);
        }
        private void Fill(int value) {
            for (int i = 0; i < Items.Count; i++)
                Items[i].sprite = states[Mathf.Clamp(value - i * StateFactor, 0, StateFactor)];
        }


        private void Clear(int max) {
            for (int i = Items.Count; i > 0; i--)
                if (i > max / StateFactor)
                    Destroy(Items[i]);
        }
    }
}