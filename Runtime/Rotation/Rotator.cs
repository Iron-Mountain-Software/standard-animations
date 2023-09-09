using UnityEngine;

namespace IronMountain.StandardAnimations.Rotation
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
        private Quaternion _initialGlobalRotation;
        private Quaternion _initialLocalRotation;
        private float _frameMultiplier;

        private void Awake()
        {
            _initialGlobalRotation = transform.rotation;
            _initialLocalRotation = transform.localRotation;
        }

        private void Reset()
        {
            if (pivot == Space.Self)
            {
                transform.localRotation = _initialLocalRotation;
            }
            else transform.rotation = _initialGlobalRotation;
        }

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