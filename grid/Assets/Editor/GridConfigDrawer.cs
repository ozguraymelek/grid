#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Source.Data.GridConfig))]
    public class GridConfigDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var sizeProp = serializedObject.FindProperty("sizeDigit");
            var maxDigitsProp = serializedObject.FindProperty("MaxSizeDigit");

            EditorGUILayout.PropertyField(maxDigitsProp);

            var maxDigits = maxDigitsProp.intValue;
            const int min = 1;
            var max = Mathf.RoundToInt(Mathf.Pow(10, maxDigits)) - 1;

            var clamped = EditorGUILayout.IntSlider("Size", sizeProp.intValue, min, max);
            sizeProp.intValue = clamped;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif