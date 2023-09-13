using System;
using System.Collections;
using UnityEngine;

namespace IronMountain.StandardAnimations.Scale
{
    public class ScaleUpAndDownAnimator : StandardAnimation
    {
        [SerializeField] private Vector3 minimumScale = Vector3.zero;
        [SerializeField] private Vector3 maximumScale = Vector3.one;

        public override void Enter() =>
            ScaleUp(seconds);
        public override void Enter(Action onComplete) =>
            ScaleUp(seconds, onComplete);
        public override void Enter(float animationSeconds, Action onComplete = null) =>
            ScaleUp(animationSeconds, onComplete);
        
        [ContextMenu("Scale Up")]
        public void ScaleUp() =>
            ScaleDown(seconds);
        public void ScaleUp(Action onComplete) =>
            ScaleDown(seconds, onComplete); 
        public void ScaleUp(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(AnimationRunner(minimumScale, maximumScale, animationSeconds, onComplete));
        }

        public override void EnterImmediate() =>
            ScaleUpImmediate();
        public override void EnterImmediate(Action onComplete) =>
            ScaleUpImmediate(onComplete);
        
        [ContextMenu("Scale Up Immediate")]
        public void ScaleUpImmediate() =>
            ScaleUpImmediate(null);
        public void ScaleUpImmediate(Action onComplete)
        {
            StopAllCoroutines();
            transform.localScale = maximumScale;
            onComplete?.Invoke();
        }

        public override void Exit() =>
            ScaleDown(seconds);
        public override void Exit(Action onComplete) =>
            ScaleDown(seconds, onComplete);
        public override void Exit(float animationSeconds, Action onComplete = null) =>
            ScaleDown(animationSeconds, onComplete);
        
        [ContextMenu("Scale Down")]
        public void ScaleDown() =>
            ScaleDown(seconds);
        public void ScaleDown(Action onComplete) =>
            ScaleDown(seconds, onComplete); 
        public void ScaleDown(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(AnimationRunner(maximumScale, minimumScale, animationSeconds, onComplete));
        }

        public override void ExitImmediate() =>
            ScaleDownImmediate(null);
        public override void ExitImmediate(Action onComplete) =>
            ScaleDownImmediate(onComplete);

        [ContextMenu("Scale Down Immediate")]
        public void ScaleDownImmediate() =>
            ScaleDownImmediate(null);
        public void ScaleDownImmediate(Action onComplete)
        {
            StopAllCoroutines();
            transform.localScale = minimumScale;
            onComplete?.Invoke();
        }

        private IEnumerator AnimationRunner(Vector3 startScale, Vector3 endScale, float duration, Action onComplete)
        {
            float progress = Mathf.Min(
                Mathf.InverseLerp(startScale.x, endScale.x, transform.localScale.x),
                Mathf.InverseLerp(startScale.y, endScale.y, transform.localScale.y),
                Mathf.InverseLerp(startScale.z, endScale.z, transform.localScale.z));
            for (float timer = progress * duration; timer < duration; timer += Time.deltaTime)
            {
                progress = timer / duration;
                transform.localScale = Vector3.Lerp(startScale, endScale, progress);
                yield return null;
            }
            transform.localScale = endScale;
            onComplete?.Invoke();
        }

        
    }
}