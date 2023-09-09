using UnityEngine;

namespace IronMountain.StandardAnimations.Scale
{
    [DisallowMultipleComponent]
    public class ScalePulser : MonoBehaviour
    {
        private const float TwoPI = 6.283185f;

        private enum PulseType
        {
            Sin,
            PingPong
        }

        [SerializeField] private PulseType type = PulseType.Sin;
        [SerializeField] private float period = 1f;
        [SerializeField] private Vector3 scaleMinimum = Vector3.zero;
        [SerializeField] private Vector3 scaleMaximum = Vector3.one;

        [Header("Settings")]
        private float _progress;

        private void Update()
        {
            switch (type)
            {
                case PulseType.Sin:
                    _progress = (Mathf.Sin(Time.time * TwoPI / period) + 1) / 2;
                    break;
                case PulseType.PingPong:
                    _progress = Mathf.PingPong(Time.time / period * 2,  1);
                    break;
            }
            transform.localScale = Vector3.Lerp(scaleMinimum, scaleMaximum, _progress);
        }
    }
}