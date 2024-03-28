using DG_Pack.Base.Reactive;
using UnityEngine;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Elements.Style {
    public class FontColor : Handler<VisualElement> {
        public FontColor(string name, IReactive<Color> source) : base(name) => _source = source;

        private readonly IReactive<Color> _source;


        public override void Bind(VisualElement parent) {
            base.Bind(parent);
            _source.OnChanged += Refresh;
        }
        public override void Unbind() {
            _source.OnChanged -= Refresh;
        }

        public override void Refresh() {
            Target.style.color = _source.Value;
        }
    }
}