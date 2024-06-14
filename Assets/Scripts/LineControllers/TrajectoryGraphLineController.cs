using Attributes;
using LinearProjectiles;
using UnityEditor;
using UnityEngine;

namespace LineControllers
{
    public class TrajectoryGraphLineController : MonoBehaviour
    {
        [Tooltip("This variable is used to display a debug trajectory of the projectile in the editor.")]
        [SerializeField] private LinearProjectile debugProjectile;
        
        [Header("Linear Graph Settings")]
        [SerializeField, Expandable] private LinearGraphLine linearGraphLine;
        
        [Header("Projectile Trajectory Settings")]
        [SerializeField, Expandable] private TrajectoryLine trajectoryLine;

        private float _a;
        private float _b;
        private LinearProjectile _activeProjectile;

        /// <summary>
        /// The active projectile that will be used to calculate the trajectory.
        /// </summary>
        public LinearProjectile ActiveProjectile
        {
            set
            {
                _activeProjectile = value;
                
                if (_activeProjectile) 
                    UpdateLines();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float A
        {
            set
            {
                _a = value;
                
                UpdateLines();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float B
        {
            set
            {
                _b = value;
                
                UpdateLines();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="linearProjectile"></param>
        public void UpdateLines(float a, float b, LinearProjectile linearProjectile)
        {
            _a = a; 
            _b = b;
            _activeProjectile = linearProjectile;
            
            UpdateLines();
        }
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!debugProjectile) 
                Debug.LogWarning("No debug projectile set for trajectory graph line controller.");
            
            if (!EditorApplication.isPlaying) 
                _activeProjectile = debugProjectile;
#endif
            
            UpdateLines();
        }

        private void UpdateLines()
        {
            if (!_activeProjectile)
                return;
        
            linearGraphLine.UpdateLine(_a, _b);
            trajectoryLine.UpdateLine(_a, _b, _activeProjectile);
        }
    }
}