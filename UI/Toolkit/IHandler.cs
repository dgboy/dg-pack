using UnityEngine.UIElements;

namespace DG_Pack.UI.Toolkit {
    public interface IHandler {
        void Bind(VisualElement parent);
        void Unbind();

        void Refresh();
    }
}