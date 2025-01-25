using System;
using DG_Pack.Services.Log;

namespace DG_Pack.Services.FSM {
    public class StateMachine : IStateMachine {
        public StateMachine(IStateFactory factory) => Factory = factory;

        private IStateFactory Factory { get; }
        private IExitAbleState Current { get; set; }


        public void Enter<TState>() where TState : IState {
            var state = ChangeState<TState>();
            state.Enter();
        }
        public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload> {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public bool IsCurrent(Type type) => Current.GetType() == type;


        private TState ChangeState<TState>() where TState : IExitAbleState {
            Current?.Exit();

            var next = Factory.Create<TState>();
            DLogger.LogTransition(this, Current.Class(), next.Class());
            Current = next;

            return next;
        }
    }
}