using System;
using UnityEngine;

namespace Attributes
{
    /// <summary>
    ///     Use this property on a ScriptableObject to allow the editor to draw an expandable
    ///     area that allows for changing the values of the object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ExpandableAttribute : PropertyAttribute
    {
    }
}