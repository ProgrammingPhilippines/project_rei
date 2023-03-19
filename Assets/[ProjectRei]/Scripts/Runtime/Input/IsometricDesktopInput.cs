using UnityEngine;
using CharacterSystem;

namespace GameInput
{
    public sealed class IsometricDesktopInput : CharacterInput
    {
        #region Fields
        [SerializeField]
        private Transform m_target = null;

        [Space]

        [SerializeField]
        private StickInput m_movementInput = StickInput.Standard;

        private Vector2 m_faceDirection = Vector2.zero;
        #endregion


        #region IsometricPlayerInput Implementation
        protected override Vector2 movementDirection =>
            m_movementInput.direction;

        protected override Vector2 faceDirection
        {
            get
            {
                UpdateFaceDirection();
                return m_faceDirection;
            }
        }
        #endregion


        #region Public Method
        public void SetTarget(Transform target) =>
            m_target = target;
        #endregion


        #region Internal Methods
        private void UpdateFaceDirection()
        {
            if (cameraComponent == null || m_target == null)
                return;

            Vector3 screen2Plane =
                ProjectScreenPointToPlane(Input.mousePosition);

            Vector3 directionRelativeToTarget =
                (screen2Plane - m_target.position);

            m_faceDirection = new Vector2(directionRelativeToTarget.x,
                directionRelativeToTarget.z);
        }

        private Vector3 ProjectScreenPointToPlane(Vector3 screenPoint)
        {
            Vector3 screen2World = cameraComponent.ScreenToWorldPoint(screenPoint);
            Vector3 cameraForward = cameraComponent.transform.forward;

            float t = -(Vector3.Dot(screen2World, Vector3.up)) /
                (Vector3.Dot(cameraForward, Vector3.up));

            Vector3 result = screen2World + (cameraForward * t);

            return result;
        }
        #endregion
    }
}