using System;
using System.Collections;
using UnityEngine;

namespace IronMountain.StandardAnimations.Cameras
{
    public class CameraFieldOfViewAnimator : MonoBehaviour
    {
        [SerializeField] private Camera[] cameras;
        [SerializeField] protected float seconds = 1f;
        [SerializeField] [Range(0, 179)] private float zoomedOutFieldOfView = 70;
        [SerializeField] [Range(0, 179)] private float zoomedInFieldOfView = 50;

        public float Seconds => seconds;

        [ContextMenu("Zoom In")]
        public void ZoomIn() =>
            ZoomIn(seconds);
        public void ZoomIn(Action onComplete) =>
            ZoomIn(seconds, onComplete); 
        public void ZoomIn(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(AnimationRunner(zoomedOutFieldOfView, zoomedInFieldOfView, animationSeconds, onComplete));
        }
        
        [ContextMenu("Zoom In Immediate")]
        public void ZoomInImmediate() =>
            ZoomInImmediate(null);
        public void ZoomInImmediate(Action onComplete)
        {
            StopAllCoroutines();
            ApplyFieldOfView(zoomedInFieldOfView);
            onComplete?.Invoke();
        }
        
        [ContextMenu("Zoom Out")]
        public void ZoomOut() =>
            ZoomOut(seconds);
        public void ZoomOut(Action onComplete) =>
            ZoomOut(seconds, onComplete); 
        public void ZoomOut(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(AnimationRunner(zoomedInFieldOfView, zoomedOutFieldOfView, animationSeconds, onComplete));
        }

        [ContextMenu("Zoom Out Immediate")]
        public void ZoomOutImmediate() =>
            ZoomOutImmediate(null);
        public void ZoomOutImmediate(Action onComplete)
        {
            StopAllCoroutines();
            ApplyFieldOfView(zoomedOutFieldOfView);
            onComplete?.Invoke();
        }

        private IEnumerator AnimationRunner(float startFieldOfView, float endFieldOfView, float duration, Action onComplete)
        {
            float currentFieldOfView = cameras.Length > 0 && cameras[0] ? cameras[0].fieldOfView : 0;
            float progress = Mathf.InverseLerp(startFieldOfView, endFieldOfView, currentFieldOfView);
            for (float timer = progress * duration; timer < duration; timer += Time.deltaTime)
            {
                progress = Mathf.SmoothStep(0, 1, timer / duration);
                ApplyFieldOfView(Mathf.Lerp(startFieldOfView, endFieldOfView, progress));
                yield return null;
            }
            ApplyFieldOfView(endFieldOfView);
            onComplete?.Invoke();
        }

        private void ApplyFieldOfView(float fieldOfView)
        {
            foreach (Camera camera in cameras)
            {
                if (camera) camera.fieldOfView = fieldOfView;
            }
        }
    }
}