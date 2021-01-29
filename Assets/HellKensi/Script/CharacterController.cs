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
        Attack,
    }
    public class CharacterController : Singleton<CharacterController>
    {
        public float Speed;
        public Animator SkinnedMeshAnimator;

        public bool MoveLeft;
        public bool MoveRight;
        public bool Jump;
        public bool Attack;

        public GameObject ColliderEdgePrefab;
        [HideInInspector]
        public List<GameObject> BottomSpheres = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> FrontSpheres = new List<GameObject>();
        //[HideInInspector]
        public List<Collider> RagdollParts = new List<Collider>();
        //[HideInInspector]
        public List<Collider> CollidingParts = new List<Collider>();


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


        IEnumerator TestRagDoll()
        {
            yield return new WaitForSeconds(5f);
            RIGID_BODY.AddForce(300f * Vector3.up);
            yield return new WaitForSeconds(0.5f);
            TurnOnRagdoll();
        }

        private void Awake()
        {

            bool SwitchBack = false;
            if (!IsFacingForward()) { SwitchBack = true; }
            FaceForward(true);
            PrepareRagDoll();
            CreateEdgeCollider();
            FaceForward(!SwitchBack);

        }

        internal void MoveForward(float speed, float SpeedGraph)
        {
            //Debug.Log("moving Foward");
            transform.Translate(Vector3.forward * speed * SpeedGraph * Time.deltaTime);
        }

       

        private void PrepareRagDoll()
        {
            Collider[] childColliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach(Collider childCol in childColliders)
            {
                if(childCol.gameObject != this.gameObject)
                {
                    childCol.isTrigger = true;
                    RagdollParts.Add(childCol);
                    childCol.gameObject.AddComponent<TriggerDetector>();
                }
            }
        }

        public void TurnOnRagdoll()
        {
            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            SkinnedMeshAnimator.enabled = false; SkinnedMeshAnimator.avatar = null;

            foreach (Collider childCol in RagdollParts)
            {
                    childCol.isTrigger = false;
            }
        }

        private void CreateEdgeCollider()
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

        public void FaceForward(bool forward)
        {
            if (forward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else{
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        public bool IsFacingForward()
        {
            if(transform.forward.z > 0f) { return true; }else { return false; }
        }
    }
}
