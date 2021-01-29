using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {

        public bool MoveLeft;
        public bool MoveRight;
        public bool Jump;
        public bool ForceTransition;
        public bool Attack;
    }
}
