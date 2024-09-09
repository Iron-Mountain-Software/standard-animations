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
            if (GUILayout.Button("Flip to Front", GUILayout.MinHeight(30)))
            {
                if (_flippableRect) _flippableRect.FlipToFront();
            }
            if (GUILayout.Button("Flip to Back", GUILayout.MinHeight(30)))
            {
                if (_flippableRect) _flippableRect.FlipToBack();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Flip to Front Immediate", GUILayout.MinHeight(30)))
            {
                if (_flippableRect) _flippableRect.FlipToFrontImmediate();
            }
            if (GUILayout.Button("Flip to Back Immediate", GUILayout.MinHeight(30)))
            {
                if (_flippableRect) _flippableRect.FlipToBackImmediate();
            }
            EditorGUILayout.EndHorizontal();
            DrawDefaultInspector();
        }
    }
}