using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/MoveForward")]
    public class MoveForward : StateData
    {
        public float Speed;
        public AnimationCurve SpeedGraph;
        public float BlockDistance;

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterController control = characterState.GetCharacterController(animator);

            if (control.Jump)
            {
                animator.SetBool(TransitionParameters.Jump.ToString(), true);
            }

            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (control.MoveRight)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                if (!CheckFront(control))
                {
                    control.transform.Translate(Vector3.forward * Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
                }

            }
            if (control.MoveLeft)
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                if (!CheckFront(control))
                {
                    control.transform.Translate(Vector3.forward * Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
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

        bool CheckFront(CharacterController controller)
        {

            foreach (GameObject o in controller.FrontSpheres)
            {
                Debug.DrawRay(o.transform.position, controller.transform.forward * 0.3f, Color.red);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, controller.transform.forward, out hit, BlockDistance))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

