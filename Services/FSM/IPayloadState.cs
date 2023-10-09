namespace DG_Pack.Services.FSM {
    public interface IPayloadState<in TPayload> : IExitAbleState {
        void Enter(TPayload payload);
    }
}