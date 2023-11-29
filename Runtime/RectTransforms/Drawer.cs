using System;
using System.Collections;
using UnityEngine;

namespace IronMountain.StandardAnimations.RectTransforms
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class Drawer : StandardAnimation
    {
        public event Action OnCurrentTargetChanged;
        public event Action<float> OnMoving;

        [SerializeField] private Vector2 anchorMinOpen;
        [SerializeField] private Vector2 anchorMaxOpen;
        [SerializeField] private Vector2 anchorMinClosed;
        [SerializeField] private Vector2 anchorMaxClosed;
        
        [Header("Cache")]
        private RectTransform _rectTransform;
        private float _previousTarget;
        private float _currentTarget;

        public float PreviousTarget => _previousTarget;

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

        public override void Enter() =>
            Open(seconds);
        public override void Enter(Action onComplete) =>
            Open(seconds, onComplete);
        public override void Enter(float animationSeconds, Action onComplete = null) =>
            Open(animationSeconds, onComplete);

        [ContextMenu("Open")]
        public float Open() =>
            Open(seconds);
        public float Open(Action onComplete) =>
            Open(seconds, onComplete);
        public float Open(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            CurrentTarget = 1f;
            animationSeconds = CalculateSeconds(animationSeconds);
            if (animationSeconds <= 0 || !isActiveAndEnabled) SnapToTarget();
            else StartCoroutine(Animate(animationSeconds, onComplete));
            return animationSeconds;
        }

        public override void EnterImmediate() =>
            OpenImmediate();
        public override void EnterImmediate(Action onComplete) =>
            OpenImmediate(onComplete);
        
        [ContextMenu("Open Immediate")]
        public void OpenImmediate() =>
            OpenImmediate(null);
        public void OpenImmediate(Action onComplete)
        {
            StopAllCoroutines();
            CurrentTarget = 1f;
            SnapToTarget();
            onComplete?.Invoke();
        }

        public override void Exit() =>
            Close(seconds);
        public override void Exit(Action onComplete) =>
            Close(seconds, onComplete);
        public override void Exit(float animationSeconds, Action onComplete = null) =>
            Close(animationSeconds, onComplete);

        [ContextMenu("Close")]
        public float Close() =>
            Close(seconds);
        public float Close(Action onComplete) =>
            Close(seconds, onComplete);
        public float Close(float animationSeconds, Action onComplete = null)
        {
            StopAllCoroutines();
            CurrentTarget = 0f;
            animationSeconds = CalculateSeconds(animationSeconds);
            if (animationSeconds <= 0 || !isActiveAndEnabled) SnapToTarget();
            else StartCoroutine(Animate(animationSeconds, onComplete));
            return animationSeconds;
        }

        public override void ExitImmediate() =>
            CloseImmediate();
        public override void ExitImmediate(Action onComplete) =>
            CloseImmediate(onComplete);
        
        [ContextMenu("Close Immediate")]
        public void CloseImmediate() =>
            CloseImmediate(null);
        public void CloseImmediate(Action onComplete)
        {
            StopAllCoroutines();
            CurrentTarget = 0f;
            SnapToTarget();
            onComplete?.Invoke();
        }
        
        public float SetTarget(float target, Action onComplete = null)
        {
            StopAllCoroutines();
            CurrentTarget = target;
            float animationSeconds = CalculateSeconds(seconds);
            if (animationSeconds <= 0 || !isActiveAndEnabled) SnapToTarget();
            else StartCoroutine(Animate(animationSeconds, onComplete));
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

        private float CalculateSeconds(float animationSeconds)
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
            return (animationSeconds / baseDistance) * currentDistance;
        }

        private IEnumerator Animate(float animationSeconds, Action onComplete)
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
            onComplete?.Invoke();
        }
    }
}