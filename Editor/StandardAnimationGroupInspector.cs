using UnityEditor;
using UnityEngine;

namespace IronMountain.StandardAnimations.Editor
{
    [CustomEditor(typeof(StandardAnimationGroup), true)]
    public class StandardAnimationGroupInspector : UnityEditor.Editor
    {
        private StandardAnimationGroup _standardAnimationGroup;
        
        private void OnEnable()
        {
            _standardAnimationGroup = (StandardAnimationGroup) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Enter Immediate") && _standardAnimationGroup) _standardAnimationGroup.EnterImmediate();
            if (GUILayout.Button("Exit Immediate") && _standardAnimationGroup) _standardAnimationGroup.ExitImmediate();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Enter") && _standardAnimationGroup) _standardAnimationGroup.Enter();
            if (GUILayout.Button("Exit") && _standardAnimationGroup) _standardAnimationGroup.Exit();
            GUILayout.EndHorizontal();

            DrawDefaultInspector();
        }
    }
}