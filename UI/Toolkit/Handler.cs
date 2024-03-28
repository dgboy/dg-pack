using System;
using DG_Pack.UI.Toolkit.Query;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit {
    public abstract class Handler<T> : IHandler where T : VisualElement {
        protected Handler() : this(new BaseQuery()) { }
        protected Handler(string name) : this(new NameQuery(name)) { }
        protected Handler(IElementQuery query) => _query = query;


        private readonly IElementQuery _query;
        public T Target { get; private set; }


        public virtual void Bind(VisualElement parent) {
            if (parent == null)
                throw new NullReferenceException($"[{GetType().Name}] The parent element is empty.");

            Target = _query.Get<T>(parent);

            if (Target == null)
                throw new NullReferenceException($"[{GetType().Name}] The target element with the {_query} not found.");
        }
        public virtual void Unbind() { }
        public virtual void Refresh() { }
    }
}