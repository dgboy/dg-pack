using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Query {
    public class BaseQuery : IElementQuery {
        public T Get<T>(VisualElement root) where T : VisualElement => root.Q<T>();
        public List<T> GetMany<T>(VisualElement root) where T : VisualElement => root.Query<T>().ToList();
    }
}