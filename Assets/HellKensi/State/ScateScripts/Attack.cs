using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "CurlyGames/Abilities/Attack")]
    public class Attack : StateData
    {
        public float StartAttackTime;
        public float EndAttackTime;


        public List<string> ColliderNames = new List<string>();

        public bool MustCollide;
        public bool MustFaceAttacker;

        public float LethalRange;

        public int maxHits;
        public List<RuntimeAnimatorController> DeathAnimators = new List<RuntimeAnimatorController>();


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Debug.Log("Jab!");
            animator.SetBool(TransitionParameters.Attack.ToString(), false);

            GameObject obj = PoolManager.Instance.GetGameObject(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);

            info.ResetInfo(this, characterState.GetCharacterController(animator));

            if (!AttackManager.Instance.CurrentAttacks.Contains(info))
            {
                AttackManager.Instance.CurrentAttacks.Add(info);
            }

        }

        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
            {
                foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if(info == null) { continue; }
                    if(!info.isRegistered && info.AttackAbility == this)
                    {
                        info.Register(this);
                    }
                }
            }
        }
        public void DeRegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndAttackTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null) { continue; }
                    if (info.AttackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        //Destroy(info.gameObject);
                        info.GetComponent<PoolObject>().EndAttack();
                    }
                }
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeRegisterAttack(characterState, animator, stateInfo);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ClearAttack();
        }
        public void ClearAttack()
        {
            for(int i = 0; i < AttackManager.Instance.CurrentAttacks.Count; i++)
            {
                if(AttackManager.Instance.CurrentAttacks[i] == null || AttackManager.Instance.CurrentAttacks[i].isFinished){
                    AttackManager.Instance.CurrentAttacks.RemoveAt(i);

                }
            }
        }

        public RuntimeAnimatorController GetDeathAnimator()
        {
            return DeathAnimators[Random.Range(0, DeathAnimators.Count)];
        }
    }
}