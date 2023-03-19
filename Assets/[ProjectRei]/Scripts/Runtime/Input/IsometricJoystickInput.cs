using UnityEngine;
using CharacterSystem;

namespace GameInput
{
    public sealed class IsometricJoystickInput : CharacterInput
    {
        #region Fields
        [SerializeField]
        private StickInput m_movementInput = StickInput.Standard;
        
        [SerializeField]
        private StickInput m_headingInput = StickInput.Standard;
        #endregion


        #region IsometricPlayerInput Implementation
        protected override Vector2 faceDirection =>
            TransformRelativeToCameraDirection(m_headingInput.direction);

        protected override Vector2 movementDirection =>
            m_movementInput.direction;
        #endregion


        #region Method
        private Vector2 TransformRelativeToCameraDirection(Vector2 stickInput)
        {
            if (cameraComponent == null)
                return stickInput;

            Quaternion cameraYawRotation = GetCameraYawRotation();

            Vector3 transformedInput = cameraYawRotation *
                StickControlToWorldDirection(stickInput);

            return new Vector2(transformedInput.x, -transformedInput.z);
        }
        #endregion
    }
}