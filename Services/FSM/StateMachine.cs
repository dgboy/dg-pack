using System;
using DG_Pack.Services.Log;

namespace DG_Pack.Services.FSM {
    public class StateMachine : IStateMachine {
        public StateMachine(ICustomLogger logger, IStateFactory factory) {
            _logger = logger;
            _factory = factory;
        }

        private readonly ICustomLogger _logger;
        private readonly IStateFactory _factory;
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

            var next = _factory.Create<TState>();
            _logger.LogTransition(this, Current, next);
            Current = next;

            return next;
        }
    }
}