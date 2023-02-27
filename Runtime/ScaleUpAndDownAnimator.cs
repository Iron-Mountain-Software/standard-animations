using System.Collections;
using UnityEngine;

namespace SpellBoundAR.TransformAnimations
{
    public class ScaleUpAndDownAnimator : MonoBehaviour
    {
        [SerializeField] private Vector3 minimumScale = Vector3.zero;
        [SerializeField] private Vector3 maximumScale = Vector3.one;
        [SerializeField] private float seconds = .5f;

        public void ScaleUpImmediate()
        {
            StopAllCoroutines();
            transform.localScale = maximumScale;
        }
        
        public void ScaleUp()
        {
            StopAllCoroutines();
            StartCoroutine(AnimationRunner(minimumScale, maximumScale));
        }

        public void ScaleDownImmediate()
        {
            StopAllCoroutines();
            transform.localScale = minimumScale;
        }
        
        public void ScaleDown()
        {
            StopAllCoroutines();
            StartCoroutine(AnimationRunner(maximumScale, minimumScale));
        }

        private IEnumerator AnimationRunner(Vector3 startScale, Vector3 endScale)
        {
            float progress = Mathf.Min(
                Mathf.InverseLerp(startScale.x, endScale.x, transform.localScale.x),
                Mathf.InverseLerp(startScale.y, endScale.y, transform.localScale.y),
                Mathf.InverseLerp(startScale.z, endScale.z, transform.localScale.z));
            for (float timer = progress * seconds; timer < seconds; timer += Time.deltaTime)
            {
                progress = timer / seconds;
                transform.localScale = Vector3.Lerp(startScale, endScale, progress);
                yield return null;
            }
            transform.localScale = endScale;
        }
    }
}