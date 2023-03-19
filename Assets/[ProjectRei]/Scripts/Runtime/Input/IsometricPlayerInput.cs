using UnityEngine;

namespace GameInput
{
    [DisallowMultipleComponent]
    public abstract class IsometricPlayerInput : MonoBehaviour
    {
        #region Field
        [SerializeField]
        private Camera m_camera = null;
        #endregion


        #region Properties
        public Vector3 movement
        {
            get
            {
                Quaternion orientation = (m_camera != null) ?
                    Quaternion. Euler(Vector3.up * m_camera.transform.eulerAngles.y) :
                    Quaternion.identity;

                return orientation *
                    StickControlToWorldDirection(movementDirection);
            }
        }

        public float heading =>
            DirectionToHeading(StickControlToWorldDirection(faceDirection));

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

        private Vector3 StickControlToWorldDirection(Vector2 stickControl) =>
            new Vector3(stickControl.x, 0f, stickControl.y);

        protected float DirectionToHeading(Vector3 direction) =>
            -Mathf.Repeat(Mathf.Atan2(direction.z, direction.x) *
            Mathf.Rad2Deg, 360f) + 90f;
        #endregion
    }
}