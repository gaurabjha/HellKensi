using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    public class CharacterState : StateMachineBehaviour
    {

        public List<StateData> ListAbilityData = new List<StateData>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StateData state in ListAbilityData)
            {
                state.OnEnter(this, animator, stateInfo);
            }
        }

        public void UpdateAll(CharacterState characterState , Animator animator, AnimatorStateInfo animatorStateInfo )
        {
            foreach(StateData ability in ListAbilityData)
            {
                ability.UpdateAbility(characterState, animator, animatorStateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo);
        }

        private CharacterController _characterController;
        public CharacterController GetCharacterController(Animator animator)
        {
            if(_characterController == null)
            {
                _characterController = animator.gameObject.GetComponentInParent<CharacterController>();
            }
            return _characterController;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData state in ListAbilityData)
            {
                state.OnExit(this, animator, stateInfo);
            }
        }
    }
}