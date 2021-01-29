using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<PoolObjectType, List<GameObject>> PoolDictionary = new Dictionary<PoolObjectType, List<GameObject>>();

        public void SetUpDictionary()
        {
            PoolObjectType[] arr = System.Enum.GetValues(typeof(PoolObjectType)) as PoolObjectType[];
            foreach (PoolObjectType _poolObjectType in arr)
            {
                if (!PoolDictionary.ContainsKey(_poolObjectType))
                {
                    PoolDictionary.Add(_poolObjectType, new List<GameObject>());
                }
            }
        }

        public GameObject GetGameObject(PoolObjectType objType)
        {
            if(PoolDictionary.Count == 0) { SetUpDictionary(); }

            List<GameObject> list = PoolDictionary[objType];
            GameObject obj = null;
            if( list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
            else
            {
                obj =  PoolObjectLoader.InstantiatePrefab(objType).gameObject;
                obj.SetActive(false);
            }

            return obj;
        }

        public void AddGameObject(PoolObject obj)
        {
            List<GameObject> list = PoolDictionary[obj.poolObjectType];

            list.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }
    }
}