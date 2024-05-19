using UnityEngine;

namespace DG_Pack.UI.Canvas {
    public interface IHandler {
        void Bind(Component parent);
        void Unbind();

        void Refresh();
    }
}