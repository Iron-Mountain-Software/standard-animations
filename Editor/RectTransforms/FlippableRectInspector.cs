using IronMountain.StandardAnimations.RectTransforms;
using UnityEditor;
using UnityEngine;

namespace IronMountain.StandardAnimations.Editor.RectTransforms
{
    [CustomEditor(typeof(FlippableRect), true)]
    public class FlippableRectInspector : UnityEditor.Editor
    {
        [Header("Cache")]
        private FlippableRect _flippableRect;

        private void OnEnable()
        {
            _flippableRect = (FlippableRect) target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Flip", GUILayout.MinHeight(30)))
            {
                if (_flippableRect) _flippableRect.Flip();
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Frontside", GUILayout.MinHeight(30)))
            {
                if (_flippableRect) _flippableRect.FlipToFront();
            }
            if (GUILayout.Button("Backside", GUILayout.MinHeight(30)))
            {
                if (_flippableRect) _flippableRect.FlipToBack();
            }
            EditorGUILayout.EndHorizontal();
            DrawDefaultInspector();
        }
    }
}