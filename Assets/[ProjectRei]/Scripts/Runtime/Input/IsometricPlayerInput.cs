using UnityEngine;

namespace GameInput
{
    [DisallowMultipleComponent]
    public abstract class IsometricPlayerInput : MonoBehaviour
    {
        public abstract Vector2 movement { get; }
        public abstract float heading { get; }
    }
}