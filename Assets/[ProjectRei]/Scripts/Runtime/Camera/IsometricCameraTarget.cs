using UnityEngine;

namespace GameCamera
{
    [RequireComponent(typeof(IsometricCamera))]
    [DisallowMultipleComponent]
    public sealed class IsometricTarget : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private IsometricCamera m_isometricCamera = null;

        [SerializeField]
        private Transform m_target = null;

        [Space]

        [SerializeField, Min(MinDistanceThreshold)]
        private float m_dampThreshold = 10f;

        [SerializeField]
        private AnimationCurve m_dampTransitionCurve =
            AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Space]

        [SerializeField, Min(0f)]
        private float m_distalDampTime = 0.5f;

        [SerializeField, Min(0f)]
        private float m_proximalDampTime = 1f;

        private const float MinDistanceThreshold = 0f;
        private Vector3 m_dampVelocity = Vector3.zero;
        #endregion


        #region MonoBehaviour Implementation
        private void Reset()
        {
            if (m_isometricCamera == null)
                m_isometricCamera = GetComponent<IsometricCamera>();
        }

        private void FixedUpdate() => TrackTarget();
        #endregion


        #region Internal Methods
        private void TrackTarget()
        {
            if (m_isometricCamera == null || m_target == null)
                return;

            Vector3 currentPoint = m_isometricCamera.pivot;
            Vector3 targetPoint = m_target.position;
            float dampTime = CalculateDampTime(currentPoint, targetPoint);

            m_isometricCamera.pivot = CalculateSmoothPivotPoint(currentPoint,
                targetPoint, dampTime);
        }

        private float CalculateDampTime(Vector3 currentPoint, Vector3 targetPoint)
        {
            float distance = Vector3.Distance(currentPoint, targetPoint);
            float t = Mathf.InverseLerp(MinDistanceThreshold, m_dampThreshold, distance);

            return Mathf.LerpUnclamped(m_proximalDampTime,
                m_distalDampTime, m_dampTransitionCurve.Evaluate(t));
        }

        private Vector3 CalculateSmoothPivotPoint(Vector3 currentPoint, Vector3 targetPoint, float dampTime) =>
            Vector3.SmoothDamp(currentPoint, targetPoint, ref m_dampVelocity, dampTime);
        #endregion
    }
}