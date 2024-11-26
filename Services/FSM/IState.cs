namespace DGPack.Services.FSM {
    public interface IState : IExitAbleState {
        void Enter();
    }
}