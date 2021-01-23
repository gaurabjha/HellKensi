using UnityEngine;
using UnityEditor;

namespace HellKensi
{
    [CustomEditor(typeof(CharacterController))]
    public class MaterialChanger : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CharacterController controller = (CharacterController)target;

            if(GUILayout.Button("Change Material"))
            {
                controller.ChangeMaterial();
            }

        }
    }
}

