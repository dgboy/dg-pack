using DG_Pack.Base.Reactive;
using UnityEngine;

namespace DG_Pack.UI.Canvas.Handlers {
    public class Active : Handler<Component> {
        public Active(string name, IReactive<bool> source, bool inverse = false) : base(name) {
            _inverse = inverse;
            _source = source;
        }

        private readonly IReactive<bool> _source;
        private readonly bool _inverse;


        public override void Bind(Component parent) {
            base.Bind(parent);
            _source.OnChanged += Refresh;
        }
        public override void Unbind() {
            _source.OnChanged -= Refresh;
        }

        public override void Refresh() {
            Target.gameObject.SetActive(_inverse ? !_source.Value : _source.Value);
        }
    }
}