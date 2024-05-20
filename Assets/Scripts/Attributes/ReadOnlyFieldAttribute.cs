using System;
using UnityEngine;

namespace Attributes
{
    /// <summary>
    /// Use this attribute to make fields read-only in the editor (e.g. debug values).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyFieldAttribute : PropertyAttribute
    {
    }
}