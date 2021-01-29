using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/MoveForward")]
    public class MoveForward : StateData
    {
        public bool Reflex;
        public float Speed;
        public AnimationCurve SpeedGraph;
        public float BlockDistance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Jump.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterController control = characterState.GetCharacterController(animator);

            if (control.Jump)
            {
                animator.SetBool(TransitionParameters.Jump.ToString(), true);
            }

            if (Reflex) { ReflexMove(control, animator, stateInfo); }
            else { ControledMove(control, animator, stateInfo); }

        }

        private void ReflexMove(CharacterController control, Animator animator, AnimatorStateInfo stateInfo)
        {
            control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
        }


        private void ControledMove(CharacterController control, Animator animator, AnimatorStateInfo stateInfo)
        {
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
                    control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }

            }
            if (control.MoveLeft)
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                if (!CheckFront(control))
                {
                    control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Attack.ToString(), false);
        }

        bool CheckFront(CharacterController controller)
        {

            foreach (GameObject o in controller.FrontSpheres)
            {
                Debug.DrawRay(o.transform.position, controller.transform.forward * 0.3f, Color.cyan);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, controller.transform.forward, out hit, BlockDistance))
                {
                    if (!controller.RagdollParts.Contains(hit.collider))
                    {
                        if (!IsBodyPart(hit.collider))
                        {
                            //Debug.Log("Obstracle " + hit.collider.gameObject.name);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        bool IsBodyPart(Collider col)
        {
            CharacterController control = col.transform.root.GetComponent<CharacterController>();

            if (control == null)
            {
                return false;
            }

            if( control.gameObject == col.gameObject)
            {
                return false;
            }

            if(control.RagdollParts.Contains(col))
            {
                return true;
            }

            return false;
        }
    }
}

