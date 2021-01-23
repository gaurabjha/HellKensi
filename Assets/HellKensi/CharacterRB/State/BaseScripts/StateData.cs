using UnityEngine;


namespace HellKensi
{

    public abstract class StateData : ScriptableObject
    {
        public float Duration;
        public float Speed;

        public abstract void UpdateAll(CharacterState characterState, Animator animator);
    }
}
