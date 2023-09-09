using System;
using System.Collections;
using UnityEngine;

namespace IronMountain.StandardAnimations.CanvasGroups
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupAnimator : MonoBehaviour
    {
        [SerializeField] private float seconds = 1f;
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

        public float Seconds => seconds;

        // Parameterless for UnityEvents
        public void FadeIn() => FadeIn(seconds);
        public void FadeIn(Action onComplete) => FadeIn(seconds, onComplete);
        public void FadeIn(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(Animate(alphaMinimum, alphaMaximum, animationSeconds, onComplete));
        }
        
        // Parameterless for UnityEvents
        public void FadeInImmediate() => FadeInImmediate(null);
        public void FadeInImmediate(Action onComplete)
        {
            StopAllCoroutines();
            CanvasGroup.alpha = alphaMaximum;
            onComplete?.Invoke();
        }

        // Parameterless for UnityEvents
        public void FadeOut() => FadeOut(seconds);
        public void FadeOut(Action onComplete) => FadeOut(seconds, onComplete);
        public void FadeOut(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(Animate(alphaMaximum, alphaMinimum, seconds, onComplete));
        }
    
        // Parameterless for UnityEvents
        public void FadeOutImmediate() => FadeOutImmediate(null);
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