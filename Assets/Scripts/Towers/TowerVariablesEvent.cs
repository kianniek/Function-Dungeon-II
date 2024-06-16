using System;
using UnityEngine.Events;

namespace Towers
{
    /// <summary>
    /// Event that passes TowerVariables
    /// </summary>
    [Serializable]
    public class TowerVariablesEvent : UnityEvent<TowerVariables>
    {
    }
}