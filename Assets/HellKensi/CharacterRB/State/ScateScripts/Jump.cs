using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/Jump")]
    public class Jump : StateData
    {
        public float JumpForce;
        public AnimationCurve Gravity;
        public AnimationCurve Pull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.GetCharacterController(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
            animator.SetBool(TransitionParameters.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            CharacterController control = characterState.GetCharacterController(animator);
            control.GravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);
            control.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);
        }


        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}

