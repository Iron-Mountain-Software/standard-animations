using IronMountain.StandardAnimations.RectTransforms;
using UnityEditor;
using UnityEngine;

namespace IronMountain.StandardAnimations.Editor.RectTransforms
{
    [CustomEditor(typeof(Drawer), true)]
    public class DrawerInspector : UnityEditor.Editor
    {
        private Drawer _drawer;

        private void OnEnable()
        {
            _drawer = (Drawer) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Immediate") && _drawer) _drawer.OpenImmediate();
            if (GUILayout.Button("Close Immediate") && _drawer) _drawer.CloseImmediate();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open") && _drawer) _drawer.Open();
            if (GUILayout.Button("Close") && _drawer) _drawer.Close();
            GUILayout.EndHorizontal();

            DrawDefaultInspector();
        }
    }
}