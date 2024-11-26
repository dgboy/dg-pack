namespace DGPack.Services.FSM {
    public interface IStateFactory {
        TState Create<TState>() where TState : IExitAbleState;
    }
}