using UnityEngine;

namespace Progression.Grading
{
    [CreateAssetMenu(fileName = "Grade", menuName = "Level/Grade")]
    public class Grade : ScriptableObject
    {
        [SerializeField] private string gradeName;
        [SerializeField] private Texture2D icon;
        
        public string GradeName => gradeName;
        
        public Texture2D Icon => icon;
    }
}