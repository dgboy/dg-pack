using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.UI.Canvas.Query {
    public class SimpleQuery : IElementQuery {
        public T Get<T>(Component root) where T : Component => root as T;

        public IEnumerable<T> GetMany<T>(Component root) where T : Component =>
            root.transform.GetComponents<T>();
    }
}