using System;
using System.Collections;
using UnityEngine;

namespace IronMountain.StandardAnimations.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CameraFieldOfViewAnimator : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] protected float seconds = 1f;
        [SerializeField] [Range(0, 179)] private float zoomedOutFieldOfView = 70;
        [SerializeField] [Range(0, 179)] private float zoomedInFieldOfView = 50;

        public float Seconds => seconds;

        private void OnEnable()
        {
            if (!camera) camera = GetComponent<Camera>();
        }

        private void OnValidate()
        {
            if (!camera) camera = GetComponent<Camera>();
        }

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
            if (camera) camera.fieldOfView = zoomedInFieldOfView;
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
            if (camera) camera.fieldOfView = zoomedOutFieldOfView;
            onComplete?.Invoke();
        }

        private IEnumerator AnimationRunner(float startFieldOfView, float endFieldOfView, float duration, Action onComplete)
        {
            float currentFieldOfView = camera ? camera.fieldOfView : 0;
            float progress = Mathf.InverseLerp(startFieldOfView, endFieldOfView, currentFieldOfView);
            for (float timer = progress * duration; timer < duration; timer += Time.deltaTime)
            {
                progress = Mathf.SmoothStep(0, 1, timer / duration);
                if (camera) camera.fieldOfView = Mathf.Lerp(startFieldOfView, endFieldOfView, progress);
                yield return null;
            }
            if (camera) camera.fieldOfView = endFieldOfView;
            onComplete?.Invoke();
        }
    }
}