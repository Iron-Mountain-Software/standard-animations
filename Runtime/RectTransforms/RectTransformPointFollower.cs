using UnityEngine;

namespace ARISE.Utilities.UI.UIFollow
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformPointFollower : MonoBehaviour
    {
        public RectTransform target;
        
        [Header("Settings")]
        [SerializeField] private Vector2 targetPivotToFollow = Vector2.one * .5f;
        [SerializeField] private Vector3 pixelOffset = Vector3.zero;

        [Header("Cache")]
        private RectTransform _rectTransform;

        private RectTransform RectTransform
        {
            get
            {
                if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        public void LateUpdate()
        {
            if (!target) return;
            Vector3 targetPosition = target.position;
            Vector3 targetLocalScale = target.localScale;
            Rect targetRect = target.rect;
            float halfTargetWidth = targetRect.width * targetLocalScale.x / 2;
            float halfTargetHeight = targetRect.height * targetLocalScale.y / 2;
            RectTransform.position =
                new Vector3(
                    targetPosition.x + pixelOffset.x + Mathf.LerpUnclamped(-halfTargetWidth, halfTargetWidth, targetPivotToFollow.x),
                    targetPosition.y + pixelOffset.y + Mathf.LerpUnclamped(-halfTargetHeight, halfTargetHeight, targetPivotToFollow.y),
                    targetPosition.z + pixelOffset.z);
            RectTransform.rotation = target.rotation;
        }
    }
}
