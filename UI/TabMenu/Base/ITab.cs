namespace DG_Pack.UI.TabMenu.Base {
    public interface ITab {
        bool Active { get; set; }
        void SetGroup(ITabMenu value);
        void Select(bool on, bool sendEvent = false);
    }
}