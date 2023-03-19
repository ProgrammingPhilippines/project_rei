using UnityEngine;

namespace GameInput
{
    public sealed class OnscreenGamepadInput : GamepadInput
    {
        #region Properties
        protected override Vector2 leftJoystickControl => throw new System.NotImplementedException();
        protected override Vector2 rightJoystickControl => throw new System.NotImplementedException();
        #endregion
    }
}