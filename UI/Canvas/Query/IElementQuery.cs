using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.UI.Canvas.Query {
    public interface IElementQuery {
        T Get<T>(Component root) where T : Component;
        IEnumerable<T> GetMany<T>(Component root) where T : Component;
    }
    public class ElementQuery : IElementQuery {
        public T Get<T>(Component root) where T : Component =>
            throw new System.NotImplementedException();
        public IEnumerable<T> GetMany<T>(Component root) where T : Component =>
            throw new System.NotImplementedException();
    }
}