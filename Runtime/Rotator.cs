using UnityEngine;

namespace SpellBoundAR.TransformAnimations
{
    public class Rotator : MonoBehaviour
    {
        private enum TimeScale
        {
            Scaled,
            Unscaled
        }

        [Range(-1.0f, 1.0f)] public float xForceDirection = 0.0f;
        [Range(-1.0f, 1.0f)] public float yForceDirection = 0.0f;
        [Range(-1.0f, 1.0f)] public float zForceDirection = 0.0f;

        public float speedMultiplier = 1;

        [SerializeField] private TimeScale timeScale = TimeScale.Scaled;
        [SerializeField] private Space pivot = Space.Self;

        [Header("Cache")]
        private Quaternion _initialRotation;
        private float _frameMultiplier;

        private void OnEnable() => _initialRotation = transform.rotation;
        private void OnDisable() => transform.rotation = _initialRotation;

        private void Update()
        { 
            _frameMultiplier = timeScale == TimeScale.Scaled
                ? speedMultiplier * Time.deltaTime
                : speedMultiplier * Time.unscaledDeltaTime;
            transform.Rotate(
                _frameMultiplier * xForceDirection,
                _frameMultiplier * yForceDirection,
                _frameMultiplier * zForceDirection,
                pivot);
        }
    }
}