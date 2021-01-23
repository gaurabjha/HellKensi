using UnityEngine;
using UnityEditor;

namespace HellKensi
{
    [CustomEditor(typeof(MaterialChanger))]
    public class MaterialChangerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MaterialChanger materialChanger = (MaterialChanger)target;

            if(GUILayout.Button("Change Material"))
            {
                materialChanger.ChangeMaterial();
            }

        }
    }
}

