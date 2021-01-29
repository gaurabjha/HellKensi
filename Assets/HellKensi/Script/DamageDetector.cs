using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    public class DamageDetector : MonoBehaviour
    {
        CharacterController control;
        

        private void Awake()
        {
            control = GetComponent<CharacterController>();
        }


        private void Update()
        {
            if(AttackManager.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }
        private void CheckAttack()
        {
            foreach( AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if(info == null)
                {
                    continue;
                }
                if (info.Attacker == control || !info.isRegistered || info.isFinished || info.CurrentHits >= info.maxHits)
                {
                    continue;
                }

                if (info.MustCollide)
                {
                    if (IsCollided(info))
                    {
                        TakeDamege(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info)
        {
            foreach(Collider collider in control.CollidingParts)
            {
                foreach(string name in info.ColliderNames)
                {
                    if( name == collider.name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void TakeDamege(AttackInfo info)
        {
            Debug.Log(info.Attacker.gameObject.name + " Hits : " + this.gameObject.name);
            control.SkinnedMeshAnimator.runtimeAnimatorController = info.AttackAbility.GetDeathAnimator();
            info.CurrentHits++;

            control.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;
            //control.TurnOnRagdoll();
        }
    }

}

