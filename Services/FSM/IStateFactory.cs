namespace DG_Pack.Services.FSM {
    public interface IStateFactory {
        TState Create<TState>() where TState : IExitAbleState;
    }
}