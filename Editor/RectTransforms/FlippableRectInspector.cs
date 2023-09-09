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
                _flippableRect.Flip();
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Frontside", GUILayout.MinHeight(30)))
            {
                if (_flippableRect.FrontSide) _flippableRect.FrontSide.SetActive(true);
                if (_flippableRect.BackSide) _flippableRect.BackSide.SetActive(false);
            }
            if (GUILayout.Button("Backside", GUILayout.MinHeight(30)))
            {
                if (_flippableRect.FrontSide) _flippableRect.FrontSide.SetActive(false);
                if (_flippableRect.BackSide) _flippableRect.BackSide.SetActive(true);
            }
            EditorGUILayout.EndHorizontal();
            DrawDefaultInspector();
        }
    }
}