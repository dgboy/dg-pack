using DG_Pack.Base.Reactive;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Elements.Text {
    public class Text : Handler<TextElement> {
        public Text(string name, IReactive<string> source) : base(name) => _source = source;

        private readonly IReactive<string> _source;


        public override void Bind(VisualElement parent) {
            base.Bind(parent);
            _source.OnChanged += Refresh;
        }
        public override void Unbind() {
            _source.OnChanged -= Refresh;
        }

        public override void Refresh() {
            Target.text = _source.Value;
        }
    }
}