using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HellKensi
{
    public enum TransitionParameters
    {
        Move,
    }
    public class CharacterController : Singleton<CharacterController>
    {
        public float Speed;
        public Animator animator;

        public Material material;

        public void ChangeMaterial()
        {
            if( material == null)
            {
                Debug.LogError("No Material Supplied");
            }
            Renderer[] renderers  = this.gameObject.GetComponentsInChildren<Renderer>();

            foreach(Renderer renderer in renderers)
            {
                if( this.gameObject != renderer.gameObject)
                {
                    renderer.material = material;
                }
            }
        }
    }
}
