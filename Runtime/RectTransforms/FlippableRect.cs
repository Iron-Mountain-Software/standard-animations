using System;
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
        private Coroutine _flipCoroutine;

        public GameObject FrontSide => frontSide;
        public GameObject BackSide => backSide;

        private void Start()
        {
            if (frontSide) frontSide.SetActive(true);
            if (backSide) backSide.SetActive(false);
            _flipCoroutine = null;
        }

        private void OnDisable()
        {
            _flipCoroutine = null;
        }

        [ContextMenu("Flip")]
        public void Flip()
        {
            _flipCoroutine ??= StartCoroutine(FlipRunner());
        }

        public void FlipToFront()
        {
            if (frontSide.activeSelf && !backSide.activeSelf) return;
            _flipCoroutine ??= StartCoroutine(FlipRunner());
        }

        public void FlipToBack()
        {
            if (backSide.activeSelf && !frontSide.activeSelf) return;
            _flipCoroutine ??= StartCoroutine(FlipRunner());
        }

        private IEnumerator FlipRunner()
        {
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
                frontSide.SetActive(!frontSide.activeInHierarchy);
                if (frontSide.activeInHierarchy) onFrontSide?.Invoke();
            }

            if (backSide)
            {
                backSide.SetActive(!backSide.activeInHierarchy);
                if (backSide.activeInHierarchy) onBackSide?.Invoke();
            }
            
            for (float i = 0; i < halfDuration; i += Time.unscaledDeltaTime) {
                transform.rotation = Quaternion.Lerp(halfRotation, startRotation, i / halfDuration);
                yield return null;
            }

            transform.rotation = startRotation;

            _flipCoroutine = null;
        }
    }
}