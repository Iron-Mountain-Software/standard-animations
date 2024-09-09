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
        
        private enum Side
        {
            None,
            Front,
            Back
        }

        [Header("Settings")]
        [SerializeField] private Axis axis;
        [SerializeField] private Space space = Space.World;
        [SerializeField] private Side startSide = Side.Front;

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
            switch (startSide)
            {
                case Side.Front:
                    if (frontSide) frontSide.SetActive(true);
                    if (backSide) backSide.SetActive(false);
                    break;
                case Side.Back:
                    if (frontSide) frontSide.SetActive(false);
                    if (backSide) backSide.SetActive(true);
                    break;
            }
            _flipping = false;
        }

        private void OnDisable()
        {
            _flipping = false;
        }

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
        
        public void FlipToFrontImmediate()
        {
            StopAllCoroutines();
            if (frontSide) frontSide.SetActive(true);
            if (backSide) backSide.SetActive(false);
            onFrontSide?.Invoke();
            SetRotation(Quaternion.Euler(0, 0, 0));
        }

        public void FlipToBack()
        {
            if (!_flipping
                && (!frontSide || !frontSide.activeSelf)
                && (!backSide || backSide.activeSelf)) return;
            StopAllCoroutines();
            StartCoroutine(FlipRunner(true, false));
            _flipping = false;
        }
        
        public void FlipToBackImmediate()
        {
            StopAllCoroutines();
            if (frontSide) frontSide.SetActive(false);
            if (backSide) backSide.SetActive(true);
            onBackSide?.Invoke();
            SetRotation(Quaternion.Euler(0, 0, 0));
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
                SetRotation(Quaternion.Lerp(startRotation, halfRotation, i / halfDuration));
                yield return null;
            }

            SetRotation(halfRotation);

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
            
            for (float i = 0; i < halfDuration; i += Time.unscaledDeltaTime)
            {
                SetRotation(Quaternion.Lerp(halfRotation, startRotation, i / halfDuration));
                yield return null;
            }

            SetRotation(startRotation);

            _flipping = false;
        }

        private void SetRotation(Quaternion rotation)
        {
            if (space == Space.Self)
            {
                transform.localRotation = rotation;
            }
            else transform.rotation = rotation;
        }
    }
}