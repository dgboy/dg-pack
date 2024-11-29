namespace DG_Pack.Services.FSM {
    public interface IState : IExitAbleState {
        void Enter();
    }
}