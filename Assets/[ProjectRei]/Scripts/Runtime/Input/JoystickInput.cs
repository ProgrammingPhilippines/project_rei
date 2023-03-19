using UnityEngine;

namespace GameInput
{
    public sealed class JoystickInput : BaseJoystickInput
    {
        #region Fields
        [SerializeField]
        private StickControl m_leftJoystick = new StickControl("Move X", "Move Y");

        [SerializeField]
        private StickControl m_rightJoystick = new StickControl("Face X", "Face Y");
        #endregion


        #region BaseJoystickInput Implementation
        protected override Vector2 leftJoystickControl =>
            m_leftJoystick.direction;

        protected override Vector2 rightJoystickControl =>
            m_rightJoystick.direction;
        #endregion
    }
}