using DG_Pack.Base.Reactive;
using UnityEngine;
using UnityEngine.UI;

namespace DG_Pack.UI.Canvas.Handlers {
    public class FloatSlider : Handler<Slider> {
        public FloatSlider(string name, IReactive<float> source, bool inverse = false) : base(name) {
            _inverse = inverse;
            _source = source;
        }

        private readonly IReactive<float> _source;
        private readonly bool _inverse;


        public override void Bind(Component parent) {
            base.Bind(parent);
            _source.OnChanged += Refresh;
        }
        public override void Unbind() {
            _source.OnChanged -= Refresh;
        }

        public override void Refresh() {
            Target.value = _inverse ? 1f - _source.Value : _source.Value;
        }
    }
}