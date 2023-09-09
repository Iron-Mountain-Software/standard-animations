using UnityEngine;

namespace IronMountain.StandardAnimations.CanvasGroups
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupAnimateByPosition : MonoBehaviour
    {
        [Tooltip("Which transform does this effect happen in reference to? Defaults to this transform.")]
        public Transform ReferenceTransform;
        public RectTransform.Axis axis = RectTransform.Axis.Horizontal;
        [Range(0, 1)] public float peakOpacity = 1f;
        [Range(0, 1)] public float antiPeakOpacity;

        [Tooltip("At what percent of the visible screen is the item at it's alpha peak? (0 = left/top edge, 1 = right/bottom edge)")]
        public float PeakScreenPercent;

        [Tooltip("How many screens away is this effect at it's antipeak?")]
        public float ScreenRangePercent;

        [Header("Cache")]
        private CanvasGroup _canvasGroup;
        private float _peakScreenPixel;
        private float _screenRangePixels;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (!ReferenceTransform) ReferenceTransform = transform;
        }

        private void Start()
        {
            switch (axis)
            {
                case RectTransform.Axis.Horizontal:
                    _peakScreenPixel = Screen.width * PeakScreenPercent;
                    _screenRangePixels = Screen.width * ScreenRangePercent;
                    break;
                case RectTransform.Axis.Vertical:
                    _peakScreenPixel = Screen.height * PeakScreenPercent;
                    _screenRangePixels = Screen.height * ScreenRangePercent;
                    break;
            }
        }

        private void Update()
        {
            if (!_canvasGroup || !ReferenceTransform) return;
            float value = axis == RectTransform.Axis.Horizontal
                ? ReferenceTransform.position.x
                : ReferenceTransform.position.y;
            float diff = Mathf.Abs(value - _peakScreenPixel);
            float proximity = (_screenRangePixels - diff) / _screenRangePixels;
            _canvasGroup.alpha = Mathf.Lerp(antiPeakOpacity, peakOpacity, proximity);
        }
    }
}