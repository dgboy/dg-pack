using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DG_Pack.UI.Canvas.Query {
    public class NameQuery : IElementQuery {
        public NameQuery(string key) => _key = key;

        private readonly string _key;


        public T Get<T>(Component root) where T : Component {
            return root.transform.FirstOrDefault(x => x.name == _key)?.GetComponent<T>();
                // .GetComponentsInChildren<T>(true)
                // .First(x => x.name == _key);
        }

        public IEnumerable<T> GetMany<T>(Component root) where T : Component =>
            root.gameObject
                .GetComponentsInChildren<T>(true)
                .Where(x => x.name == _key);


        public override string ToString() => $"name '#{_key}'";
    }
}