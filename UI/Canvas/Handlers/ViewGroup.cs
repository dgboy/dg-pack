using DG_Pack.Base.Reactive;
using UnityEngine;

namespace DG_Pack.UI.Canvas.Handlers {
    public class ViewGroup : Handler<Component> {
        public ViewGroup(string name, IReactive<int> source) : base(name) {
            _source = source;
        }

        private readonly IReactive<int> _source;
        private readonly bool _inverse;


        public override void Bind(Component parent) {
            base.Bind(parent);

            _source.OnChanged += Refresh;
        }
        public override void Unbind() {
            _source.OnChanged -= Refresh;
        }

        public override void Refresh() {
            var parent = Target.transform;
            
            for (int i = 0; i < parent.childCount; i++) {
                parent.GetChild(i).gameObject.SetActive(_source.Value == i);
            }
        }
    }
}