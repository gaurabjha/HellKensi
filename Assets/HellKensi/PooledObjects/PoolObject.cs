using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType poolObjectType;
        public float ScheduleEndAttackTime;
        private Coroutine EndAttackScheduler;


        private void OnEnable()
        {
            if (EndAttackScheduler != null)
            {
                StopCoroutine(EndAttackScheduler);
            }
            if (ScheduleEndAttackTime > 0f) { EndAttackScheduler = StartCoroutine(_ScheduleEndAttack()); }
        }

        public void EndAttack()
        {
            PoolManager.Instance.AddGameObject(this);
        }

        IEnumerator _ScheduleEndAttack()
        {
            yield return new WaitForSeconds(ScheduleEndAttackTime);
            if (!PoolManager.Instance.PoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                EndAttack();
            }
        }
    }
}
