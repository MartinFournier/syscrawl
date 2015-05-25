using UnityEngine;
using UnityEditor;
using syscrawl.Levels;

namespace syscrawl.Editors
{
    [CustomEditor(typeof(Generator))]
    public class GeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate Level"))
                (target as Generator).GenerateLevel();
        }
    }
}