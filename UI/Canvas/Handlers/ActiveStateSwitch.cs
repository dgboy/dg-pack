using DG_Pack.Base.Reactive;
using UnityEngine;

namespace DG_Pack.UI.Canvas.Handlers {
    public class ActiveStateSwitch : Handler<Component> {
        public ActiveStateSwitch(string name, IReactive<bool> source) : base(name) {
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
            var parent = Target.transform;
            parent.GetChild(0).gameObject.SetActive(_source.Value);
            parent.GetChild(1).gameObject.SetActive(!_source.Value);
        }
    }
}