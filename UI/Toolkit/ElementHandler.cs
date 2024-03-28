using System;
using DG_Pack.UI.Toolkit.Query;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit {
    public abstract class ElementHandler : IHandler {
        protected ElementHandler() : this(new BaseQuery()) { }
        protected ElementHandler(string name) : this(new NameQuery(name)) { }
        protected ElementHandler(IElementQuery query) => _query = query;


        private readonly IElementQuery _query;
        public VisualElement Target { get; private set; }


        public virtual void Bind(VisualElement parent) {
            if (parent == null)
                throw new NullReferenceException($"[{GetType().Name}] The parent element is empty.");

            Target = _query.Get<VisualElement>(parent);

            if (Target == null)
                throw new NullReferenceException($"[{GetType().Name}] The target element with the {_query} not found.");
        }
        public virtual void Unbind() { }
        public virtual void Refresh() { }
    }
}