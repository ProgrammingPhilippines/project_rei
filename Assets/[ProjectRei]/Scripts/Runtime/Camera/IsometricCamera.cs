using UnityEngine;

namespace GameCamera
{
    [DisallowMultipleComponent]
    public sealed class IsometricCamera : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private Camera m_camera = null;

        [SerializeField]
        private Quadrant m_quadrant = Quadrant.Quadrant1;

        [SerializeField, Range(0f, 90f)]
        private float m_pitch = 45f;

        [SerializeField, Min(1f)]
        private float m_orthoSize = 10f;

        [SerializeField]
        private Vector3 m_pivot = Vector3.zero;
        #endregion


        #region Data Structure
        [System.Serializable]
        private enum Quadrant
        {
            Quadrant1 = 0,
            Quadrant2 = 1,
            Quadrant3 = 2,
            Quadrant4 = 3
        }
        #endregion


        #region Properties
        public float pitch
        {
            get => m_pitch;
            set => m_pitch = Mathf.Clamp(value, 0f, 90f);
        }

        public Vector3 pivot
        {
            get => m_pivot;
            set => m_pivot = value;
        }

        public float orthoSize
        {
            get => m_orthoSize;
            set => m_orthoSize = Mathf.Max(0f, value);
        }
        #endregion


        #region MonoBehaviour Implementation
        private void FixedUpdate()
        {
            UpdateTransformation();
            UpdateCamera();
        }
        #endregion


        #region Internal Methods
        private void UpdateCamera()
        {
            if (m_camera != null)
                m_camera.orthographicSize = m_orthoSize;
        }

        private void UpdateTransformation()
        {
            float yaw = SolveYaw(m_quadrant);
            Vector3 orbitDirection = SolveOrbitDirection(m_pitch, yaw);
            float distanceRelativeToOrthoSize = SolveDistance(m_orthoSize);

            Vector3 finalPosition = m_pivot + (orbitDirection * distanceRelativeToOrthoSize);
            Quaternion finalRotation = SolveOrbitRotation(m_pitch, yaw);
            transform.SetPositionAndRotation(finalPosition, finalRotation);
        }
        #endregion


        #region Helper Methods
        private float SolveYaw(Quadrant quadrant)
        {
            const float RightAngle = 90f;

            return (RightAngle / 2f) + (RightAngle * (int)quadrant);
        }
        
        private float SolveDistance(float orthoSize)
        {
            float aspectRatio = (Screen.width / Screen.height);

            return orthoSize * aspectRatio * 7f;
        }

        private Vector3 SolveOrbitDirection(float pitch, float yaw)
        {
            float pitchRadians = pitch * Mathf.Deg2Rad;
            float yawRadians = yaw * Mathf.Deg2Rad;

            float w = Mathf.Cos(pitchRadians),
                x = Mathf.Cos(yawRadians) * w,
                y = Mathf.Sin(pitchRadians),
                z = Mathf.Sin(yawRadians) * w;

            return new Vector3(x, y, z);
        }

        private Quaternion SolveOrbitRotation(float pitch, float yaw) =>
            Quaternion.Euler(pitch, -yaw - 90f, 0f);
        #endregion
    }
}