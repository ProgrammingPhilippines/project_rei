using UnityEngine;

namespace GameInput
{
    [DisallowMultipleComponent]
    public abstract class IsometricPlayerInput : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private Camera m_camera = null;

        [SerializeField]
        private float m_faceDirectionDeadZone = 0.5f;

        private float m_heading = 0f;
        #endregion


        #region Properties
        public Vector3 movement =>
            GetCameraYawRotation() *
            StickControlToWorldDirection(movementDirection);

        public float heading
        {
            get
            {
                UpdateHeading();
                return m_heading;
            }
        }

        protected Camera cameraComponent => m_camera;
        protected abstract Vector2 faceDirection { get; }
        protected abstract Vector2 movementDirection { get; }
        #endregion


        #region MonoBehaviour Implementation
        protected virtual void Reset()
        {
            if (m_camera == null)
                m_camera = Camera.main;
        }
        #endregion


        #region Methods
        public void SetCamera(Camera camera) =>
            m_camera = camera;

        private void UpdateHeading()
        {
            Vector2 currentFaceDirection = this.faceDirection;

            if (currentFaceDirection.magnitude <= m_faceDirectionDeadZone)
                return;

            m_heading = DirectionToHeading(StickControlToWorldDirection(currentFaceDirection));
        }
        #endregion


        #region Helpers
        protected Vector3 StickControlToWorldDirection(Vector2 stickControl) =>
            new Vector3(stickControl.x, 0f, stickControl.y);

        protected Quaternion GetCameraYawRotation()
        {
            if (m_camera == null)
                return Quaternion.identity;

            return Quaternion.Euler(Vector3.up * m_camera.transform.eulerAngles.y);
        }

        protected float DirectionToHeading(Vector3 direction) =>
            -Mathf.Repeat(Mathf.Atan2(direction.z, direction.x) *
            Mathf.Rad2Deg, 360f) + 90f;
        #endregion
    }
}