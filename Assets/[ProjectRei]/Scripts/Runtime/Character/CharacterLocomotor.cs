using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class CharacterLocomotor : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private Rigidbody m_rigidbody = null;

        [SerializeField, Min(0.01f)]
        private float m_movementSpeed = 10f;

        [SerializeField, Range(0f, 1f)]
        private float m_turnSpeed = 0.5f;

        private Vector3 m_velocity = Vector3.zero;
        private Quaternion m_rotation = Quaternion.identity;

        private const int HeadingMaxQueue = 5;
        private List<float> m_headings = new List<float>();

        private const float MinTurnSpeed = 360f,
            MaxTurnSpeed = 2160f,
            HeadingThreshold = 5f;
        #endregion


        #region MonoBehaviour Implementation
        private void Reset()
        {
            if (m_rigidbody == null)
                m_rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() => UpdateHeading();

        private void FixedUpdate()
        {
            ApplyLocomotion();
            ClearVelocity();
        }
        #endregion


        #region Public Methods
        public void Move(Vector3 direction) =>
            m_velocity = (direction * m_movementSpeed);

        public void Steer(float heading)
        {
            bool replaceLastRecordedHeading = HeadingsHistoryIsAlreadyFull() ||
                IsSimilarToLastRecordedHeading(heading);

            if (replaceLastRecordedHeading)
                m_headings[m_headings.Count - 1] = heading;

            else m_headings.Add(heading);
        }
        #endregion


        #region Internal Methods
        private void UpdateHeading()
        {
            if (m_headings.Count <= 0)
                return;

            Quaternion targetHeading = Quaternion.Euler(Vector3.up * m_headings[0]);
            float turnSpeed = Mathf.Lerp(MinTurnSpeed, MaxTurnSpeed, m_turnSpeed);

            m_rotation = Quaternion.RotateTowards(m_rotation, targetHeading, Time.deltaTime * turnSpeed);

            if (Mathf.Approximately(Quaternion.Angle(m_rotation, targetHeading), 0f))
                m_headings.RemoveAt(0);
        }

        private void ApplyLocomotion()
        {
            if (m_rigidbody == null)
                return;

            Vector3 position = m_rigidbody.position +
                (m_velocity * Time.fixedDeltaTime);

            m_rigidbody.MovePosition(position);
            m_rigidbody.MoveRotation(m_rotation);
        }

        private void ClearVelocity() =>
            m_velocity = Vector3.zero;
        #endregion


        #region Helper Methods
        private bool HeadingsHistoryIsAlreadyFull() =>
            (m_headings.Count >= HeadingMaxQueue);

        private bool IsSimilarToLastRecordedHeading(float heading)
        {
            if (m_headings.Count <= 0)
                return false;

            return HeadingsAreAlmostSimilar(heading, m_headings[m_headings.Count - 1]);
        }

        private bool HeadingsAreAlmostSimilar(float a, float b) =>
            (Mathf.Abs(a - b) <= HeadingThreshold);
        #endregion
    }
}