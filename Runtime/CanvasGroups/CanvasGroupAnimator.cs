using System;
using System.Collections;
using UnityEngine;

namespace IronMountain.StandardAnimations.CanvasGroups
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupAnimator : StandardAnimation
    {
        [SerializeField] private float alphaMinimum = 0f;
        [SerializeField] private float alphaMaximum = 1f;

        [Header("Cache")]
        private CanvasGroup _canvasGroup;

        private CanvasGroup CanvasGroup 
        {
            get
            {
                if (!_canvasGroup) _canvasGroup = GetComponent<CanvasGroup>();
                return _canvasGroup;
            }
        }

        public override void Enter() =>
            FadeIn(seconds);
        public override void Enter(Action onComplete) =>
            FadeIn(seconds, onComplete);
        public override void Enter(float animationSeconds, Action onComplete = null) =>
            FadeIn(animationSeconds, onComplete);
        
        [ContextMenu("Fade In")]
        public void FadeIn() =>
            FadeIn(seconds);
        public void FadeIn(Action onComplete) =>
            FadeIn(seconds, onComplete);
        public void FadeIn(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(Animate(alphaMinimum, alphaMaximum, animationSeconds, onComplete));
        }

        public override void EnterImmediate() =>
            FadeInImmediate(null);
        public override void EnterImmediate(Action onComplete) =>
            FadeInImmediate(onComplete);
        
        [ContextMenu("Fade In Immediate")]
        public void FadeInImmediate() =>
            FadeInImmediate(null);
        public void FadeInImmediate(Action onComplete)
        {
            StopAllCoroutines();
            CanvasGroup.alpha = alphaMaximum;
            onComplete?.Invoke();
        }

        public override void Exit() =>
            FadeOut(seconds);
        public override void Exit(Action onComplete) =>
            FadeOut(seconds, onComplete);
        public override void Exit(float animationSeconds, Action onComplete = null) =>
            FadeOut(animationSeconds, onComplete);
        
        [ContextMenu("Fade Out")]
        public void FadeOut() =>
            FadeOut(seconds);
        public void FadeOut(Action onComplete) =>
            FadeOut(seconds, onComplete);
        public void FadeOut(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(Animate(alphaMaximum, alphaMinimum, seconds, onComplete));
        }

        public override void ExitImmediate() =>
            FadeOutImmediate(null);
        public override void ExitImmediate(Action onComplete) =>
            FadeOutImmediate(onComplete);
        
        [ContextMenu("Fade Out Immediate")]
        public void FadeOutImmediate() =>
            FadeOutImmediate(null);
        public void FadeOutImmediate(Action onComplete)
        {
            StopAllCoroutines();
            CanvasGroup.alpha = alphaMinimum;
            onComplete?.Invoke();
        }
    
        private IEnumerator Animate(float startValue, float endValue, float duration, Action onComplete)
        {
            float currentValue = CanvasGroup.alpha;
            float progress = Mathf.InverseLerp(startValue, endValue, currentValue);
            for (float timer = progress * duration; timer < duration; timer += Time.unscaledDeltaTime)
            {
                progress = timer / duration;
                CanvasGroup.alpha = Mathf.Lerp(startValue, endValue, progress);
                yield return null;
            }
            CanvasGroup.alpha = endValue;
            onComplete?.Invoke();
        }
    }
}