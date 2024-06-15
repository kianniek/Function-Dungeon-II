using System;
using System.Collections.Generic;
using UnityEngine;

namespace LinearFunction
{
    /// <summary>
    /// Represents a collection of X values for the linear function.
    /// </summary>
    [Serializable]
    public struct XValues
    {
        [SerializeField] private float n1;
        [SerializeField] private float n2;
        [SerializeField] private float n3;
        [SerializeField] private float n4;
        [SerializeField] private float n5;
        [SerializeField] private float n6;

        /// <summary>
        /// Gets the values of the X values.
        /// </summary>
        public IReadOnlyList<float> Values => new[] {n1, n2, n3, n4, n5, n6};
    }
}