using IronMountain.StandardAnimations.CanvasGroups;
using UnityEditor;
using UnityEngine;

namespace IronMountain.StandardAnimations.Editor.CanvasGroups
{
    [CustomEditor(typeof(CanvasGroupAnimator), true)]
    public class CanvasGroupAnimatorInspector : UnityEditor.Editor
    {
        [Header("Cache")]
        private CanvasGroupAnimator _canvasGroupAnimator;
        
        private void OnEnable()
        {
            _canvasGroupAnimator = (CanvasGroupAnimator) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Fade In") && _canvasGroupAnimator) _canvasGroupAnimator.FadeIn();
            if (GUILayout.Button("Fade Out") && _canvasGroupAnimator) _canvasGroupAnimator.FadeOut();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Fade In Immediate") && _canvasGroupAnimator) _canvasGroupAnimator.FadeInImmediate();
            if (GUILayout.Button("Fade Out Immediate") && _canvasGroupAnimator) _canvasGroupAnimator.FadeOutImmediate();
            GUILayout.EndHorizontal();
        }
    }
}
