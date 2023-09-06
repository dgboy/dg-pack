using UnityEngine;
using UnityEngine.UI;

namespace DG_Pack.UI.Bars.Points {
    public class HealthPointFactory : IPointFactory {
        private Transform Parent { get; }
        private Image Prefab { get; }
        private object Config { get; set; }


        public HealthPointFactory(Image prefab, Transform parent) {
            Parent = parent;
            Prefab = prefab;
        }

        public Image Create() {
            var point = Object.Instantiate(Prefab, Parent);
            point.name = $"point {Parent.childCount}";
            return point;
        }
    }
}