using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellKensi
{
    public class MaterialChanger : Singleton<MaterialChanger>
    {
        public Material material;

        public void ChangeMaterial()
        {
            if (material == null)
            {
                Debug.LogError("No Material Supplied");
            }
            Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (this.gameObject != renderer.gameObject)
                {
                    renderer.material = material;
                }
            }
        }
    }
}
