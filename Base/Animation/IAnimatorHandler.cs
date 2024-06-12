namespace DG_Pack.Base.Animation {
    public interface IAnimatorHandler {
        // PlayerState State { get; set; }
        void EnterState(int stateHash);
        void ExitState(int stateHash);
    }
}