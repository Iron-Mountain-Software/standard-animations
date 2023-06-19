using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace SpellBoundAR.TransformAnimations
{
    [ExecuteAlways]
    public class LocalAimConstraint : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 rotationAxis = Vector3.right;
        [SerializeField] private Vector3 centerAxis = Vector3.forward;
        [SerializeField] private Vector3 upAxis = Vector3.up;
        [SerializeField] private float range = 180f;

        [Header("Editor")]
        [SerializeField] private Color color = new Color(1,1,1, .25f);
        [SerializeField] private float radius = 1f;

        [Header("Cache")]
        private Transform _transform;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        private void OnEnable()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (!target) return;
            Vector3 myPosition = _transform.position;
            Vector3 lookVector = target.position - myPosition;
            Vector3 axis = CalculateAxis(rotationAxis);
            Vector3 center = CalculateAxis(centerAxis);
            Vector3 up = CalculateAxis(upAxis);
            lookVector = Vector3.ProjectOnPlane(lookVector, axis);
            float angle = Vector3.SignedAngle(center, lookVector, axis);
            angle = Mathf.Clamp(angle, -range, range);
            Vector3 result = Quaternion.AngleAxis(angle, axis) * center;
            _transform.LookAt(myPosition + result, up);
        }

        private Vector3 CalculateAxis(Vector3 transformations)
        {
            Transform parent = _transform ? _transform.parent : null;
            return parent
                ? (parent.right * transformations.x
                   + parent.up * transformations.y
                   + parent.forward * transformations.z).normalized
                : transformations.normalized;
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmosSelected()
        {
            Handles.color = color;
            Vector3 myPosition = _transform.position;
            Vector3 axis = CalculateAxis(rotationAxis);
            Vector3 center = CalculateAxis(centerAxis);
            Gizmos.DrawRay(myPosition, axis);
            Handles.DrawSolidArc(myPosition, axis, center, range, radius);
            Handles.DrawSolidArc(myPosition, axis, center, -range, radius);
        }
        
#endif

    }
}
