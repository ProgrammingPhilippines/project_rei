using UnityEngine;
using CharacterSystem;

namespace GameInput
{
    public sealed class JoystickInput : CharacterInput
    {
        #region Fields
        [SerializeField]
        private StickControl m_movementInput = StickControl.Standard;
        
        [SerializeField]
        private StickControl m_headingInput = StickControl.Standard;
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