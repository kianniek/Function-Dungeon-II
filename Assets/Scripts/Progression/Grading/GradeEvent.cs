using System;
using UnityEngine.Events;

namespace Progression.Grading
{
    /// <summary>
    /// Event that passes a grade.
    /// </summary>
    [Serializable]
    public class GradeEvent : UnityEvent<Grade>
    {
    }
}