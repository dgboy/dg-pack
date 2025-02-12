namespace DG_Pack.Services.Scene {
    public interface ILoadingCurtain {
        bool Playing { get; }

        public void Show();
        public void Hide();
    }
}