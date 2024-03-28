using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Elements.Input {
    public class Click : Handler<VisualElement> {
        public Click(string name, Action action = null) : base(name) => _action = action;

        private readonly Action _action;


        public override void Bind(VisualElement parent) {
            base.Bind(parent);
            Target.RegisterCallback<ClickEvent>(HandleClick);
        }
        public override void Unbind() {
            Target.UnregisterCallback<ClickEvent>(HandleClick);
        }

        private void HandleClick(ClickEvent evt) {
            if (_action != null) _action();
            else Debug.Log($"[{GetType().Name}] clicking on <{Target.name}> - WIP");
        }
    }
}