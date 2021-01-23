using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/MoveForward" )]
    public class MoveForward : StateData
    {
        public override void UpdateAll(CharacterState CharacterState, Animator animator)
        {
            CharacterController characterController = CharacterState.GetCharacterController(animator);
            if (VirtualInputManager.Instance.MoveRight && VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (!VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                characterController.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                characterController.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            if (VirtualInputManager.Instance.MoveLeft)
            {
                characterController.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                characterController.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            }
        }
    }
}

