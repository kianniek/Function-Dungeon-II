using System;
using MaterialSystem;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Events
{
    /// <summary>
    /// One argument int-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class IntEvent : UnityEvent<int>
    {
    }

    /// <summary>
    /// One argument float-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class FloatEvent : UnityEvent<float>
    {
    }

    /// <summary>
    /// One argument double-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class DoubleEvent : UnityEvent<double>
    {
    }

    /// <summary>
    /// One argument bool-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }

    /// <summary>
    /// One argument string-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class StringEvent : UnityEvent<string>
    {
    }

    /// <summary>
    /// One argument char-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class CharEvent : UnityEvent<char>
    {
    }

    /// <summary>
    /// One argument <see cref="Vector2"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class Vector2Event : UnityEvent<Vector2>
    {
    }

    /// <summary>
    /// One argument <see cref="Vector3"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class Vector3Event : UnityEvent<Vector3>
    {
    }

    /// <summary>
    /// One argument <see cref="Vector4"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class Vector4Event : UnityEvent<Vector4>
    {
    }

    /// <summary>
    /// One argument <see cref="GameObject"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject>
    {
    }

    /// <summary>
    /// One argument <see cref="UnityEngine.Object"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class ObjectEvent : UnityEvent<Object>
    {
    }

    /// <summary>
    /// One argument <see cref="Color"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class ColorEvent : UnityEvent<Color>
    {
    }

    /// <summary>
    /// One argument <see cref="UnityEngine.UI.Button"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class ButtonEvent : UnityEvent<UnityEngine.UI.Button>
    {
    }

    /// <summary>
    /// One argument <see cref="ButtonExtended.ExtendedButton"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class ExtendedButtonEvent : UnityEvent<ButtonExtended.ExtendedButton>
    {
    }
    
    /// <summary>
    /// One argument <see cref="Item"/>-<see cref="UnityEvent"/>, serializable to the inspector.
    /// </summary>
    [Serializable]
    public class ItemEvent : UnityEvent<Item>
    {
    }
}