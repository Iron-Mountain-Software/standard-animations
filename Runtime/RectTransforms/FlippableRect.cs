using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace IronMountain.StandardAnimations.RectTransforms
{
    public class FlippableRect : MonoBehaviour
    {
        private enum Axis
        {
            Vertical,
            Horizontal
        }

        [Header("Settings")]
        [SerializeField] private Axis axis;
    
        [Header("References")]
        [SerializeField] private GameObject frontSide;
        [SerializeField] private GameObject backSide;
        [Space]
        [SerializeField] private UnityEvent onFrontSide;
        [SerializeField] private UnityEvent onBackSide;

        [Header("Cache")]
        private bool _flipping;

        public GameObject FrontSide => frontSide;
        public GameObject BackSide => backSide;
        
        private void Start()
        {
            if (frontSide) frontSide.SetActive(true);
            if (backSide) backSide.SetActive(false);
            _flipping = false;
        }

        private void OnDisable()
        {
            _flipping = false;
        }

        [ContextMenu("Flip")]
        public void Flip()
        {
            if (_flipping) return;
            StartCoroutine(FlipRunner());
        }

        public void FlipToFront()
        {
            if (!_flipping
                && (!frontSide || frontSide.activeSelf)
                && (!backSide || !backSide.activeSelf)) return;
            StopAllCoroutines();
            StartCoroutine(FlipRunner(true, true));
        }

        public void FlipToBack()
        {
            if (!_flipping
                && (!frontSide || !frontSide.activeSelf)
                && (!backSide || backSide.activeSelf)) return;
            StopAllCoroutines();
            StartCoroutine(FlipRunner(true, false));
        }

        private IEnumerator FlipRunner(bool force = false, bool front = true)
        {
            _flipping = true;
            float halfDuration = .1f;
        
            Quaternion startRotation = Quaternion.Euler(0, 0, 0);
            Quaternion halfRotation = axis == Axis.Horizontal 
                ? Quaternion.Euler(90f, 0f, 0f) 
                : Quaternion.Euler(0f, 90f, 0f);

            for (float i = 0; i < halfDuration; i += Time.unscaledDeltaTime)
            {
                transform.rotation = Quaternion.Lerp(startRotation, halfRotation, i / halfDuration);
                yield return null;
            }

            transform.rotation = halfRotation;

            if (frontSide)
            {
                if (force) frontSide.SetActive(front);
                else frontSide.SetActive(!frontSide.activeInHierarchy);
                if (frontSide.activeInHierarchy) onFrontSide?.Invoke();
            }

            if (backSide)
            {
                if (force) backSide.SetActive(!front);
                else backSide.SetActive(!backSide.activeInHierarchy);
                if (backSide.activeInHierarchy) onBackSide?.Invoke();
            }
            
            for (float i = 0; i < halfDuration; i += Time.unscaledDeltaTime) {
                transform.rotation = Quaternion.Lerp(halfRotation, startRotation, i / halfDuration);
                yield return null;
            }

            transform.rotation = startRotation;

            _flipping = false;
        }
    }
}