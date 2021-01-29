using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/GroundDetector")]
    public class GroundDetector : StateData
    {
        [Range(0.01f, 1f)]
        public float CheckTime;
        public float Distance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //animator.SetBool(TransitionParameters.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= CheckTime)
            {
                CharacterController controller = characterState.GetCharacterController(animator);
                if (IsGrounded(controller))
                {
                    animator.SetBool(TransitionParameters.Grounded.ToString(), true);
                }
                else
                {
                    animator.SetBool(TransitionParameters.Grounded.ToString(), false);
                }
            }
        }


        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        bool IsGrounded(CharacterController controller)
        {
            if (controller.RIGID_BODY.velocity.y > -0.011f && controller.RIGID_BODY.velocity.y <= 0f)
            {
                return true;
            }

            if (controller.RIGID_BODY.velocity.y < 0f)
            {
                foreach (GameObject o in controller.BottomSpheres)
                {
                    Debug.DrawRay(o.transform.position, -Vector3.up * 0.75f, Color.red);
                    RaycastHit hit;
                    if (Physics.Raycast(o.transform.position, -Vector3.up, out hit, Distance))
                    {
                        if (!controller.RagdollParts.Contains(hit.collider)) { return true; }
                    }
                }
            }
            return false;
        }
    }
}

