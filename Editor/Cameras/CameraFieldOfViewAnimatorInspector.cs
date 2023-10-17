using IronMountain.StandardAnimations.Cameras;
using UnityEditor;
using UnityEngine;

namespace IronMountain.StandardAnimations.Editor.Cameras
{
    [CustomEditor(typeof(CameraFieldOfViewAnimator), true)]
    public class CameraFieldOfViewAnimatorInspector : UnityEditor.Editor
    {
        private CameraFieldOfViewAnimator _cameraFieldOfViewAnimator;

        private void OnEnable()
        {
            _cameraFieldOfViewAnimator = (CameraFieldOfViewAnimator) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Zoom In Immediate")) _cameraFieldOfViewAnimator.ZoomInImmediate();
            if (GUILayout.Button("Zoom Out Immediate")) _cameraFieldOfViewAnimator.ZoomOutImmediate();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Zoom In")) _cameraFieldOfViewAnimator.ZoomIn();
            if (GUILayout.Button("Zoom Out")) _cameraFieldOfViewAnimator.ZoomOut();
            GUILayout.EndHorizontal();
            base.OnInspectorGUI();
        }
    }
}