using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit.Query {
    public interface IElementQuery {
        T Get<T>(VisualElement root) where T : VisualElement;
        List<T> GetMany<T>(VisualElement root) where T : VisualElement;
    }
}