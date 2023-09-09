using IronMountain.StandardAnimations.Scale;
using UnityEditor;
using UnityEngine;

namespace IronMountain.StandardAnimations.Editor.Scale
{
    [CustomEditor(typeof(ScaleUpAndDownAnimator), true)]
    public class ScaleUpAndDownAnimatorInspector : UnityEditor.Editor
    {
        private ScaleUpAndDownAnimator _scaleUpAndDownAnimator;

        private void OnEnable()
        {
            _scaleUpAndDownAnimator = (ScaleUpAndDownAnimator) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Scale Up Immediate")) _scaleUpAndDownAnimator.ScaleUpImmediate();
            if (GUILayout.Button("Scale Down Immediate")) _scaleUpAndDownAnimator.ScaleDownImmediate();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Scale Up")) _scaleUpAndDownAnimator.ScaleUp();
            if (GUILayout.Button("Scale Down")) _scaleUpAndDownAnimator.ScaleDown();
            GUILayout.EndHorizontal();
            base.OnInspectorGUI();
        }
    }
}
