using UnityEngine;

namespace Progression.Grading
{
    /// <summary>
    /// Represents a grade that can be assigned to a player based on their performance.
    /// </summary>
    [CreateAssetMenu(fileName = "Grade", menuName = "Progression/Grading/Grade")]
    public class Grade : ScriptableObject
    {
        [SerializeField] private string gradeName;
        [SerializeField] private Texture2D icon;
        
        /// <summary>
        /// The name of the grade.
        /// </summary>
        public string GradeName => gradeName;
        
        /// <summary>
        /// The icon representing the grade.
        /// </summary>
        public Texture2D Icon => icon;
    }
}