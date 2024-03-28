using DG_Pack.Base.Reactive;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Elements.Text {
    public class VarText<T> : Handler<TextElement> {
        public VarText(string name, IReactive<T> source) : base(name) => _source = source;

        private readonly string _name;
        private readonly IReactive<T> _source;


        public override void Bind(VisualElement parent) {
            base.Bind(parent);
            _source.OnChanged += Refresh;
        }

        public override void Unbind() {
            _source.OnChanged -= Refresh;
        }

        public override void Refresh() {
            Target.text = $"{_source.Value}";
        }
    }
}