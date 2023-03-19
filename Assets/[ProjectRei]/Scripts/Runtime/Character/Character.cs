using System;
using UnityEngine;
using CombatSystem;

namespace CharacterSystem
{
    [DisallowMultipleComponent]
    public sealed class Character : MonoBehaviour, ICombatant
    {
        #region Fields
        [SerializeField, Min(1)]
        private int m_hitPoints = 100;

        [Space]

        [SerializeField]
        private CharacterInput m_input = null;

        [SerializeField]
        private CharacterLocomotor m_locomotor = null;

        private int m_currentHitPoints = 0;
        
        #endregion


        #region Events
        public event Action Revived;
        public event Action Died;
        public event Action<int> Damaged;
        public event Action<int> Healed;
        public event HitPointEventHandler HitPointsUpdated;
        #endregion


        #region Properties
        public int currentHP => m_currentHitPoints;
        public int totalHP => m_hitPoints;
        public bool isAlive => (m_currentHitPoints > 0);
        #endregion


        #region MonoBehaviour Implementation
        private void Start() => ResetCurrentHP();
        private void Update() => UpdateLocomotion();
        #endregion


        #region Public Methods
        public void ReduceHitPoints(int value)
        {
            if (!isAlive)
                return;

            value = Mathf.Max(value, 0);
            m_currentHitPoints -= value;
            Damaged?.Invoke(value);
            HitPointsUpdated?.Invoke(m_currentHitPoints, m_hitPoints);

            if (!isAlive)
                Died?.Invoke();
        }

        public void RestoreHitPoints(int value)
        {
            if (!isAlive || m_currentHitPoints >= m_hitPoints)
                return;

            value = Mathf.Max(value, 0);
            m_currentHitPoints = Mathf.Min(m_hitPoints + value, m_hitPoints);
            Healed?.Invoke(value);
            HitPointsUpdated?.Invoke(m_currentHitPoints, m_hitPoints);
        }
        #endregion


        #region Internal Methods
        private void ResetCurrentHP()
        {
            bool wasDead = !isAlive;
            m_currentHitPoints = m_hitPoints;

            if (wasDead)
                Revived?.Invoke();
        }
 
        private void UpdateLocomotion()
        {
            if (!isAlive || m_input == null || m_locomotor == null)
                return;

            m_locomotor.Move(m_input.movement);
            m_locomotor.Steer(m_input.heading);
        }
        #endregion
    }
}