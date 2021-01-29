using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{

    public class ManualInput : MonoBehaviour
    {
        CharacterController controller;

        private void Awake()
        {
            controller = this.gameObject.GetComponent<CharacterController>();
        }
        // Update is called once per frame
        void Update()
        {
            if (VirtualInputManager.Instance.MoveRight) { controller.MoveRight = true; } else { controller.MoveRight = false; }
            if (VirtualInputManager.Instance.MoveLeft) { controller.MoveLeft = true; } else { controller.MoveLeft = false; }
            if (VirtualInputManager.Instance.Jump) { controller.Jump = true; } else { controller.Jump = false; }
            if (VirtualInputManager.Instance.Attack) { controller.Attack = true; } else { controller.Attack = false; }
        }
    }

}