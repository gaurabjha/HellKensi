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
        [HideInInspector]
        public List<GameObject> FrontSpheres = new List<GameObject>();


        public float GravityMultiplier;
        public float PullMultiplier;

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


            //Bottong Edge Collider
            GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
            GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));


            bottomBack.transform.parent = this.transform;
            bottomFront.transform.parent = this.transform;
            topFront.transform.parent = this.transform;
            


            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);


            FrontSpheres.Add(topFront);
            FrontSpheres.Add(bottomFront);

            float sec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5;
            CreateMiddleSpheres(bottomBack, this.transform.forward, sec, 4, BottomSpheres);

            sec = (topFront.transform.position - bottomFront.transform.position).magnitude / 10;
            CreateMiddleSpheres(bottomFront, this.transform.up, sec, 9, FrontSpheres);


        }

        private void FixedUpdate()
        {
            if(RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity += (-Vector3.up * GravityMultiplier);
            }

            if (RIGID_BODY.velocity.y > 0f && !Jump)
            {
                RIGID_BODY.velocity += (-Vector3.up * PullMultiplier);
            }
        }

        public void CreateMiddleSpheres(GameObject start , Vector3 dir, float sec, int iterations, List<GameObject> spheresList)
        {
            for (int i = 0; i < iterations; i++)
            {
                Vector3 pos = start.transform.position + (dir * sec * (i + 1));
                GameObject gameObject = CreateEdgeSphere(pos);
                gameObject.transform.parent = this.transform;
                spheresList.Add(gameObject);

            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);

            return obj;
        }


    }
}
