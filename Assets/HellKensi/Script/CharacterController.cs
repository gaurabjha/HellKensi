using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HellKensi
{
    public enum TransitionParameters
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
    }
    public class CharacterController : Singleton<CharacterController>
    {
        public float Speed;
        public Animator animator;

        public bool MoveLeft;
        public bool MoveRight;
        public bool Jump;

        public GameObject ColliderEdgePrefab;
        [HideInInspector]
        public List<GameObject> BottomSpheres = new List<GameObject>();

        private Rigidbody body;
        public Rigidbody RIGID_BODY
        {
            get
            {
                if (body == null)
                {
                    body = GetComponent<Rigidbody>();
                }
                return body;
            }
        }

        private void Awake()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float top = box.bounds.center.y + box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));

            bottomBack.transform.parent = this.transform;
            bottomFront.transform.parent = this.transform;

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);

            float sec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5;
            for( int i = 0; i < 4; i++)
            {
                Vector3 pos = bottomBack.transform.position + (Vector3.forward * sec * (i + 1));
                GameObject gameObject =  CreateEdgeSphere(pos);
                gameObject.transform.parent = this.transform;
                BottomSpheres.Add(gameObject);

            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);

            return obj;
        }


    }
}
