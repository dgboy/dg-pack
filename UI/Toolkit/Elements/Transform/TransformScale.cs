using DG_Pack.Base.Reactive;
using UnityEngine;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Elements.Transform {
    public class TransformScale : Handler<VisualElement> {
        public TransformScale(string name, IReactive<float> source) : base(name) => _source = source;

        private readonly IReactive<float> _source;


        public override void Bind(VisualElement parent) {
            base.Bind(parent);
            _source.OnChanged += Refresh;
        }
        public override void Unbind() {
            _source.OnChanged -= Refresh;
        }

        public override void Refresh() {
            Target.transform.scale = _source.Value * Vector3.one;
        }
    }
}