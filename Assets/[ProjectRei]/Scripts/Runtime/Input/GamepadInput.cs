using UnityEngine;
using CharacterSystem;

namespace GameInput
{
    public abstract class GamepadInput : CharacterInput
    {
        #region Properties
        protected sealed override Vector2 movementDirection =>
            leftJoystickControl;

        protected sealed override Vector2 faceDirection =>
            NormalizeJoystickInputRelativeToCamera(rightJoystickControl);

        protected abstract Vector2 leftJoystickControl { get; }
        protected abstract Vector2 rightJoystickControl { get; }
        #endregion


        #region Method
        private Vector2 NormalizeJoystickInputRelativeToCamera(Vector2 stickInput)
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