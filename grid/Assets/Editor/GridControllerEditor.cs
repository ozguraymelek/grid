#if UNITY_EDITOR
using Source.Grid;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GridController))]
    public class GridControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
// Draw default inspector fields
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime Actions", EditorStyles.boldLabel);

            // Rebuild button
            if (GUILayout.Button("Rebuild Grid"))
            {
                // Call regenerate on the target
                ((GridController)target).Regenerate();

                // Mark dirty so changes persist
                EditorUtility.SetDirty(target);

                // If editing scene (not in play mode), mark scene dirty
                if (!Application.isPlaying)
                    EditorSceneManager.MarkSceneDirty(((GridController)target).gameObject.scene);
            }
        }
    }
}
#endif
