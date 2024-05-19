using System;
using UnityEngine;
using UnityEngine.UI;

namespace DG_Pack.UI.Canvas.Handlers {
    public class Click : Handler<Button> {
        public Click(string name, Action action = null) : base(name) => _action = action;

        private readonly Action _action;


        public override void Bind(Component parent) {
            base.Bind(parent);
            Target.onClick.AddListener(HandleClick);
        }
        public override void Unbind() {
            Target.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick() {
            if (_action != null) _action();
            else Debug.Log($"[{GetType().Name}] clicking on <{Target.name}> - WIP");
        }
    }
}