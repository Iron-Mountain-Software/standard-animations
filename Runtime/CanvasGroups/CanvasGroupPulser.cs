using UnityEngine;

namespace IronMountain.StandardAnimations.CanvasGroups
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupPulser : MonoBehaviour
    {
        private const float TwoPI = 6.283185f;

        public enum PulseType
        {
            Sin,
            PingPong
        }

        [Header("Settings")]
        [SerializeField] private PulseType type = PulseType.Sin;
        [SerializeField] private float period = 1f;
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

        private void Update()
        {
            float progress = 0;
            switch (type)
            {
                case PulseType.Sin:
                    progress = (Mathf.Sin(Time.time * TwoPI / period) + 1) / 2;
                    break;
                case PulseType.PingPong:
                    progress = Mathf.PingPong(Time.time / period * 2,  1);
                    break;
            }
            CanvasGroup.alpha = Mathf.Lerp(alphaMinimum, alphaMaximum, progress);
        }
    }
}
