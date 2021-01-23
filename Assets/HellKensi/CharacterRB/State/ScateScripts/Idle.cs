using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/Idle")]
    public class Idle : StateData
    {
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            {
                CharacterController controller = characterState.GetCharacterController(animator);
                if (controller.Jump)
                {
                    animator.SetBool(TransitionParameters.Jump.ToString(), true);
                    //return;
                }
                if (controller.MoveRight)
                {
                    animator.SetBool(TransitionParameters.Move.ToString(), true);
                    //return;
                }
                if (controller.MoveLeft)
                {
                    animator.SetBool(TransitionParameters.Move.ToString(), true);
                    //return;
                }
            }
        }
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Jump.ToString(), false);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

