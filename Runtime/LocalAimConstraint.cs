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
        [SerializeField] private Vector3 centerAxis = (Vector3.up + Vector3.forward).normalized;
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
            Vector3 axis = CalculateRotationAxis();
            Vector3 center = CalculateCenterAxis();
            lookVector = Vector3.ProjectOnPlane(lookVector, axis);
            float angle = Vector3.SignedAngle(center, lookVector, axis);
            angle = Mathf.Clamp(angle, -range, range);
            Vector3 result = Quaternion.AngleAxis(angle, axis) * center;
            _transform.LookAt(myPosition + result, _transform.parent.up);
        }

        private Vector3 CalculateRotationAxis()
        {
            Transform parent = _transform ? _transform.parent : null;
            return parent
                ? (parent.right * rotationAxis.x
                   + parent.up * rotationAxis.y
                   + parent.forward * rotationAxis.z).normalized
                : rotationAxis.normalized;
        }
        
        private Vector3 CalculateCenterAxis()
        {
            Transform parent = _transform ? _transform.parent : null;
            return parent
                ? (parent.right * centerAxis.x
                   + parent.up * centerAxis.y
                   + parent.forward * centerAxis.z).normalized
                : centerAxis.normalized;
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmosSelected()
        {
            Handles.color = color;
            Vector3 myPosition = _transform.position;
            Vector3 axis = CalculateRotationAxis();
            Vector3 center = CalculateCenterAxis();
            Gizmos.DrawRay(myPosition, axis);
            Handles.DrawSolidArc(myPosition, axis, center, range, radius);
            Handles.DrawSolidArc(myPosition, axis, center, -range, radius);
        }
        
#endif

    }
}
