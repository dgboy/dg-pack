using System;

namespace DG_Pack.Services.FSM {
    public interface IStateMachine {
        void Enter<TState>() where TState : IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>;

        bool IsCurrent(Type type);
    }
}