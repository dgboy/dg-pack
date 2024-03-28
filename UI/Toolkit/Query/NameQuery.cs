using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Query {
    public class NameQuery : IElementQuery {
        public NameQuery(string key) => _key = key;

        private readonly string _key;


        public T Get<T>(VisualElement root) where T : VisualElement => root.Q<T>(_key);
        public List<T> GetMany<T>(VisualElement root) where T : VisualElement => root.Query<T>(_key).ToList();


        public override string ToString() => $"name '#{_key}'";
    }
}