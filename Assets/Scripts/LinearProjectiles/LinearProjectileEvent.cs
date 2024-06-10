using System;
using UnityEngine.Events;

namespace LinearProjectiles
{
    /// <summary>
    /// Unity event for linear projectiles.
    /// </summary>
    [Serializable]
    public class LinearProjectileEvent : UnityEvent<LinearProjectile>
    {
    }
}