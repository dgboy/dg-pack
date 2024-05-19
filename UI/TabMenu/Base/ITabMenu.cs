using System;

namespace DG_Pack.UI.TabMenu.Base {
    public interface ITabMenu {
        event Action<int, bool> OnSelect;
        int Index { get; set; }
        void Select(ITab tab, bool doAction = true);
    }
}