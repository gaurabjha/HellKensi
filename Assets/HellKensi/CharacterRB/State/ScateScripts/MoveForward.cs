using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/MoveForward")]
    public class MoveForward : StateData
    {
        public float Speed;

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterController characterController = characterState.GetCharacterController(animator);

            if (characterController.MoveRight && characterController.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (!characterController.MoveRight && !characterController.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (characterController.MoveRight)
            {
                characterController.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                characterController.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            if (characterController.MoveLeft)
            {
                characterController.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                characterController.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

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

