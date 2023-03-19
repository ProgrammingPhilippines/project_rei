using System;

namespace CombatSystem
{
    public interface ICombatant : IDamageable, IRestorable
    {
        #region Events
        event Action Revived;
        event Action Died;
        event Action<int> Damaged;
        event Action<int> Healed;
        event HitPointEventHandler HitPointsUpdated;
        #endregion


        #region Properties
        int currentHP { get; }
        int totalHP { get; }
        bool isAlive { get; }
        #endregion
    }
}