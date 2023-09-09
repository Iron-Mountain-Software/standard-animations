using System;
using System.Collections;
using UnityEngine;

namespace IronMountain.StandardAnimations.RectTransforms
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class Drawer : MonoBehaviour
    {
        public event Action OnCurrentTargetChanged;
        public event Action<float> OnMoving;

        [Header("Settings")]
        [SerializeField] private float seconds = 1f;
        [SerializeField] private Vector2 anchorMinOpen;
        [SerializeField] private Vector2 anchorMaxOpen;
        [SerializeField] private Vector2 anchorMinClosed;
        [SerializeField] private Vector2 anchorMaxClosed;
        
        [Header("Cache")]
        private RectTransform _rectTransform;
        private float _previousTarget;
        private float _currentTarget;

        public float PreviousTarget => _previousTarget;

        public float Seconds => seconds;
        
        public float CurrentTarget
        {
            get => _currentTarget;
            private set
            {
                if (_currentTarget == value) return;
                _previousTarget = _currentTarget;
                _currentTarget = value;
                OnCurrentTargetChanged?.Invoke();
            }
        }

        private RectTransform RectTransform
        {
            get
            {
                if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        public Drawer Initialize(
            float seconds,
            Vector2 anchorMinOpen,
            Vector2 anchorMaxOpen,
            Vector2 anchorMinClosed,
            Vector2 anchorMaxClosed)
        {
            this.seconds = seconds;
            this.anchorMinOpen = anchorMinOpen;
            this.anchorMaxOpen = anchorMaxOpen;
            this.anchorMinClosed = anchorMinClosed;
            this.anchorMaxClosed = anchorMaxClosed;
            return this;
        }
        

        [ContextMenu("Open")]
        public float Open()
        {
            StopAllCoroutines();
            CurrentTarget = 1f;
            float animationSeconds = CalculateSeconds();
            if (animationSeconds <= 0) SnapToTarget();
            else StartCoroutine(Animate(animationSeconds));
            return animationSeconds;
        }
        
        [ContextMenu("Open Immediate")]
        public void OpenImmediate()
        {
            StopAllCoroutines();
            CurrentTarget = 1f;
            SnapToTarget();
        }
        
        [ContextMenu("Close")]
        public float Close()
        {
            StopAllCoroutines();
            CurrentTarget = 0f;
            float animationSeconds = CalculateSeconds();
            if (animationSeconds <= 0) SnapToTarget();
            else StartCoroutine(Animate(animationSeconds));
            return animationSeconds;
        }
        
        [ContextMenu("Close Immediate")]
        public void CloseImmediate()
        {
            StopAllCoroutines();
            CurrentTarget = 0f;
            SnapToTarget();
        }
        
        public float SetTarget(float target)
        {
            StopAllCoroutines();
            CurrentTarget = target;
            float animationSeconds = CalculateSeconds();
            if (animationSeconds <= 0) SnapToTarget();
            else StartCoroutine(Animate(animationSeconds));
            return animationSeconds;
        }
        
        public void SetTargetImmediate(float target)
        {
            StopAllCoroutines();
            CurrentTarget = target;
            SnapToTarget();
        }

        private void SnapToTarget()
        {
            RectTransform.anchorMin = Vector2.Lerp(anchorMinClosed, anchorMinOpen, CurrentTarget);
            RectTransform.anchorMax = Vector2.Lerp(anchorMaxClosed, anchorMaxOpen, CurrentTarget);
            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = Vector2.zero;
        }

        private float CalculateSeconds()
        {
            if (!gameObject.activeInHierarchy) return 0;
            Vector2 startAnchorMin = RectTransform.anchorMin;
            Vector2 startAnchorMax = RectTransform.anchorMax;
            Vector2 targetAnchorMin = Vector2.Lerp(anchorMinClosed, anchorMinOpen, CurrentTarget);
            Vector2 targetAnchorMax = Vector2.Lerp(anchorMaxClosed, anchorMaxOpen, CurrentTarget);
            float baseMinDistance = Vector2.Distance(anchorMinClosed, anchorMinOpen);
            float baseMaxDistance = Vector2.Distance(anchorMaxClosed, anchorMaxOpen);
            float baseDistance = (baseMinDistance + baseMaxDistance) / 2f;
            float currentMinDistance = Vector2.Distance(startAnchorMin, targetAnchorMin);
            float currentMaxDistance = Vector2.Distance(startAnchorMax, targetAnchorMax);
            float currentDistance = (currentMinDistance + currentMaxDistance) / 2f;
            return (seconds / baseDistance) * currentDistance;
        }

        private IEnumerator Animate(float animationSeconds)
        {
            OnMoving?.Invoke(animationSeconds);
            Vector2 startAnchorMin = RectTransform.anchorMin;
            Vector2 startAnchorMax = RectTransform.anchorMax;
            Vector2 targetAnchorMin = Vector2.Lerp(anchorMinClosed, anchorMinOpen, CurrentTarget);
            Vector2 targetAnchorMax = Vector2.Lerp(anchorMaxClosed, anchorMaxOpen, CurrentTarget);
            for (float timer = 0; timer < animationSeconds; timer += Time.unscaledDeltaTime)
            {
                float progress = timer / animationSeconds;
                float smoothProgress = Mathf.SmoothStep(0, 1, progress);
                RectTransform.anchorMin = Vector2.Lerp(startAnchorMin, targetAnchorMin, smoothProgress);
                RectTransform.anchorMax = Vector2.Lerp(startAnchorMax, targetAnchorMax, smoothProgress);
                RectTransform.offsetMin = Vector2.zero;
                RectTransform.offsetMax = Vector2.zero;
                yield return null;
            }
            SnapToTarget();
        }
    }
}