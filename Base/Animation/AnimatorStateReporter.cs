using UnityEngine;

namespace DG_Pack.Base.Animation {
    public class AnimatorStateReporter : StateMachineBehaviour {
        private IAnimatorHandler _handler;


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            FindHandler(animator);
            _handler.EnterState(stateInfo.shortNameHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            FindHandler(animator);
            _handler.ExitState(stateInfo.shortNameHash);
        }

        private void FindHandler(Animator animator) {
            if (_handler != null) return;

            _handler = animator.gameObject.GetComponent<IAnimatorHandler>();
        }
    }
}