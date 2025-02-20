using UnityEngine;
using UnityEngine.UIElements;

namespace DG_Pack.Dev {
    public class DevOverlay : MonoBehaviour {
        public UIDocument document;

        private VisualElement Root => document.rootVisualElement;
        private TextElement VersionLabel { get; set; }


        private void Start() {
            VersionLabel = Root.Q("version").Q<TextElement>();
            VersionLabel.text = $"v{Application.version}";
        }
    }
}