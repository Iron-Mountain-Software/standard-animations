using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpellBoundAR.TransformAnimations
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformPositionAnimator : MonoBehaviour
    {
        [Serializable]
        private struct AnimationStep
        {
            public float beforeTravelSeconds;
            public float duringTravelSeconds;
            public float afterTravelSeconds;
            public Vector2 anchorMin;
            public Vector2 anchorMax;

            public float TravelSeconds => beforeTravelSeconds + duringTravelSeconds + afterTravelSeconds;
        }

        [SerializeField] private bool closeLoop;
        [SerializeField] private bool smooth;
        [SerializeField] private List<AnimationStep> steps = new ();
        
        private RectTransform _rectTransform;
        private int _previousStepIndex;
        private int _currentStepIndex;
        private float _timer;
        private float _progress;

        private RectTransform RectTransform
        {
            get
            {
                if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        private void OnEnable() => Reset();

        private void Reset() => SetCurrentStep(1);

        private void Update()
        {
            if (steps.Count == 0) return;
            if (_previousStepIndex < 0 || _previousStepIndex >= steps.Count
                || _currentStepIndex < 0 || _currentStepIndex >= steps.Count) Reset();
            if (_timer < steps[_currentStepIndex].beforeTravelSeconds) _progress = 0;
            else _progress = steps[_currentStepIndex].duringTravelSeconds > 0 
                ? (_timer - steps[_currentStepIndex].beforeTravelSeconds) / steps[_currentStepIndex].duringTravelSeconds
                : 1;
            if (smooth) _progress = Mathf.SmoothStep(0, 1, _progress);
            SetPosition(
                Vector2.Lerp(
                    steps[_previousStepIndex].anchorMin,
                    steps[_currentStepIndex].anchorMin,
                    _progress),
                Vector2.Lerp(
                    steps[_previousStepIndex].anchorMax,
                    steps[_currentStepIndex].anchorMax,
                    _progress));
            if (_timer < steps[_currentStepIndex].TravelSeconds) _timer += Time.deltaTime;
            else SetCurrentStep(_currentStepIndex + 1);
        }

        private void SetCurrentStep(int index)
        {
            if (index < 1)
            {
                _previousStepIndex = 0;
                _currentStepIndex = 1;
            }
            else if (index >= steps.Count)
            {
                if (closeLoop)
                {
                    _previousStepIndex = steps.Count - 1;
                    _currentStepIndex = 0;
                }
                else
                {
                    _previousStepIndex = 0;
                    _currentStepIndex = 1;
                }
            }
            else
            {
                _previousStepIndex = index - 1;
                _currentStepIndex = index;
            }
            _timer = 0;
        }

        public void SetPosition(Vector2 anchorMin, Vector2 anchorMax)
        {
            if (!RectTransform) return;
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;
            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = Vector2.zero;
        }
    }
}
