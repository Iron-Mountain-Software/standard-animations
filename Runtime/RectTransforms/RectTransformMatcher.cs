using System;
using UnityEngine;

namespace IronMountain.StandardAnimations.RectTransforms
{
    [Serializable]
    public struct Padding
    {
        [SerializeField] private float top;
        [SerializeField] private float right;
        [SerializeField] private float bottom;
        [SerializeField] private float left;

        public float Top => top;
        public float Right => right;
        public float Bottom => bottom;
        public float Left => left;
    }
    
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformMatcher : MonoBehaviour
    {
        public RectTransform target;
        
        [Header("Settings")]
        [SerializeField] private Padding padding;
        [SerializeField] private Vector3 offset;

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
            RectTransform.rotation = target.rotation;
            RectTransform.position =
                new Vector3(
                    targetPosition.x + offset.x + padding.Right * .5f - padding.Left * .5f,
                    targetPosition.y + offset.y + padding.Top * .5f - padding.Bottom * .5f,
                    targetPosition.z + offset.z);
            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (target.rect.width * targetLocalScale.x + padding.Left + padding.Right));
            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (target.rect.height * targetLocalScale.y + padding.Top + padding.Bottom));
        }
    }
}
