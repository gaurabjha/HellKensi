using System;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{

    public class AttackInfo : MonoBehaviour
    {
        public CharacterController Attacker = null;
        public Attack AttackAbility;

        public List<string> ColliderNames = new List<string>();

        public bool MustCollide;
        public bool MustFaceAttacker;

        public float LethalRange;

        public int maxHits;
        public int CurrentHits;

        public bool isRegistered;
        public bool isFinished;

        public void ResetInfo(Attack Attack, CharacterController attacker)
        {
            isRegistered = false;
            isFinished = false;
            CurrentHits = 0;
            AttackAbility = Attack;
            Attacker = attacker;
        }

        public void Register(Attack attack)
        {
            isRegistered = true;

            AttackAbility = attack;
            ColliderNames = attack.ColliderNames;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            maxHits = attack.maxHits;
        }

        private void OnDisable()
        {
            isFinished = true;
        }
    }
}