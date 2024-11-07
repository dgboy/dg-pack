using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.UI.Canvas {
    public abstract class UIView : MonoBehaviour {
        protected List<IHandler> Handlers { get; set; }


        private void Awake() {
            Init();
            Handlers.ForEach(x => x.Bind(this));
        }
        private void Start() => Handlers.ForEach(x => x.Refresh());
        private void OnDestroy() => Handlers.ForEach(x => x.Unbind());


        protected abstract void Init();
    }
}