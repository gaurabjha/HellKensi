using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    public class CharacterState : StateMachineBehaviour
    {

        public List<StateData> ListAbilityData = new List<StateData>();

        public void UpdateAll(CharacterState characterState , Animator animator)
        {
            foreach(StateData abelity in ListAbilityData)
            {
                abelity.UpdateAll(characterState, animator);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator);
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

    }
}