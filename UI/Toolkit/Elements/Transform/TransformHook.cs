using DG_Pack.Base.Reactive;
using UnityEngine;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Elements.Transform {
    public class TransformHook : Handler<VisualElement> {
        public TransformHook(string name, IReactive<Vector2> hook) : base(name) => _hook = hook;

        private readonly IReactive<Vector2> _hook;
        private Vector2 Size { get; set; }


        public override void Bind(VisualElement parent) {
            base.Bind(parent);
            _hook.OnChanged += Refresh;
            Target.RegisterCallback<GeometryChangedEvent>(HandleGeometry);
        }
        public override void Unbind() {
            _hook.OnChanged -= Refresh;
            Target.UnregisterCallback<GeometryChangedEvent>(HandleGeometry);
        }

        public override void Refresh() {
            if (Size != Vector2.zero)
                Target.transform.position = _hook.Value * Size;
        }


        private void HandleGeometry(GeometryChangedEvent evt) {
            Size = new Vector2(Target.resolvedStyle.width, Target.resolvedStyle.height);
            Refresh();
        }
    }
}