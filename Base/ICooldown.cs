namespace DG_Pack.Base {
    public interface ICooldown {
        bool IsExpired { get; }
        void Start(float time);
    }
}