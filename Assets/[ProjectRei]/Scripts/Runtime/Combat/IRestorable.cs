using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public interface IRestorable
    {
        void RestoreHitPoints(int value);
    }
}