using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    public enum PoolObjectType
    {
        ATTACKINFO,
    }
    public class PoolObjectLoader : MonoBehaviour
    {

        public static PoolObject InstantiatePrefab(PoolObjectType objType)
        {
            GameObject obj = null;
            switch (objType)
            {

                case PoolObjectType.ATTACKINFO:
                    {
                        obj = Instantiate(Resources.Load("AttackInfo") as GameObject);
                        break;
                    }
            }
            if(obj != null)
            {
                return obj.GetComponent<PoolObject>();
            }
            return null;
        }
    }
}
