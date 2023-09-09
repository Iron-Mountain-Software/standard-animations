using IronMountain.StandardAnimations.RectTransforms;
using UnityEditor;
using UnityEngine;

namespace IronMountain.StandardAnimations.Editor.RectTransforms
{
    [CustomEditor(typeof(Drawer), true)]
    public class DrawerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Immediate")) ((Drawer) target).OpenImmediate();
            if (GUILayout.Button("Close Immediate")) ((Drawer) target).CloseImmediate();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open")) ((Drawer) target).Open();
            if (GUILayout.Button("Close")) ((Drawer) target).Close();
            GUILayout.EndHorizontal();

            DrawDefaultInspector();
        }
    }
}