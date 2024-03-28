using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Query {
    public class ClassNameQuery : IElementQuery {
        public ClassNameQuery(string key) => _key = key;

        private readonly string _key;


        public T Get<T>(VisualElement root) where T : VisualElement => root.Q<T>(className: _key);
        public List<T> GetMany<T>(VisualElement root) where T : VisualElement => root.Query<T>(className: _key).ToList();


        public override string ToString() => $"class name '.{_key}'";
    }
}