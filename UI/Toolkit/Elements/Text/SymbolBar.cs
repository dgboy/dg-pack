using DG_Pack.Base.Reactive;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Elements.Text {
    public class SymbolBar : Handler<TextElement> {
        public SymbolBar(string name, IReactive<int> count, char symbol) : base(name) {
            _symbol = symbol;
            _count = count;
        }

        private readonly IReactive<int> _count;
        private readonly char _symbol;


        public override void Bind(VisualElement parent) {
            base.Bind(parent);
            Target.text = "";
            _count.OnChanged += Refresh;
        }
        public override void Unbind() {
            _count.OnChanged -= Refresh;
        }

        public override void Refresh() {
            string text = "";
            for (int i = 0; i < _count.Value; i++)
                text += _symbol;

            Target.text = text;
        }
    }
}