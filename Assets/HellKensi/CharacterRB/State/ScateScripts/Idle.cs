using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/Idle")]
    public class Idle : StateData
    {
        public override void UpdateAll(CharacterState characterState, Animator animator)
        {
            if (VirtualInputManager.Instance.MoveRight)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
                return;
            }
            if (VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
                return;
            }
        }
    }
}

