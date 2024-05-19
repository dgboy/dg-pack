using DG_Pack.Base.Reactive;
using TMPro;
using UnityEngine;

namespace DG_Pack.UI.Canvas.Handlers {
    public class TextVar<T> : Handler<TextMeshProUGUI> {
        public TextVar(string name, IReactive<T> source, string prefix = "") : base(name) {
            _prefix = prefix;
            _source = source;
        }

        private readonly IReactive<T> _source;
        private readonly string _prefix;


        public override void Bind(Component parent) {
            base.Bind(parent);
            _source.OnChanged += Refresh;
        }
        public override void Unbind() {
            _source.OnChanged -= Refresh;
        }

        public override void Refresh() {
            Target.text = $"{_prefix} {_source.Value}";
        }
    }
}