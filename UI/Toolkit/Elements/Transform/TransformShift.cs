using DG_Pack.Base.Reactive;
using UnityEngine;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Elements.Transform {
    public class TransformShift : Handler<VisualElement> {
        public TransformShift(string name, IReactive<Vector2> source) : base(name) => _source = source;

        private readonly IReactive<Vector2> _source;


        public override void Bind(VisualElement parent) {
            base.Bind(parent);
            _source.OnChanged += Set;
        }
        public override void Unbind() {
            _source.OnChanged -= Set;
        }
        
        private void Set() => Target.transform.position += (Vector3)_source.Value;
    }
}