using UnityEngine;

namespace GameInput
{
    [System.Serializable]
    public struct StickInput
    {
        #region Constructor
        public StickInput(string horizontalAxis, string verticalAxis) =>
            (m_horizontalAxis, m_verticalAxis) = (horizontalAxis, verticalAxis);
        
        public static StickInput Standard =>
            new StickInput("Horizontal", "Vertical");
        #endregion


        #region Fields
        [SerializeField]
        private string m_horizontalAxis;

        [SerializeField]
        private string m_verticalAxis;
        #endregion


        #region Properties
        public Vector2 direction => new Vector2
        (
            Input.GetAxis(m_horizontalAxis),
            Input.GetAxis(m_verticalAxis)
        );
        #endregion
    }
}