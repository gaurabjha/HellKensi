using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HellKensi
{

    public class TriggerDetector : MonoBehaviour
    {
        private CharacterController me;

        private void Awake()
        {
            me = this.GetComponentInParent<CharacterController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterController control = other.transform.root.GetComponent<CharacterController>();

            if (control == null)
            {
                return;
            }

            if (control.gameObject == other.gameObject)
            {
                return;
            }

            if (me.RagdollParts.Contains(other))
            {
                return;
            }

            if (!me.CollidingParts.Contains(other))
            {
                me.CollidingParts.Add(other);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (me.CollidingParts.Contains(other))
            {
                me.CollidingParts.Remove(other);
            }
        }
    }
}

