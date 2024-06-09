using UnityEngine;

namespace LinearProjectiles
{
    [CreateAssetMenu(fileName = "ProjectileVariables", menuName = "LinearProjectiles/Physics Projectile Data", order = 0)]
    public class ProjectilePhysicsVariables : ScriptableObject
    {
        [Tooltip("The distance traveled until gravity is applied")]
        [SerializeField] private float followDistance;
        [Tooltip("The gravity scale that will be applied when it has reached it max distance")]
        [SerializeField] private float gravity;
        [SerializeField] private float velocity;
        
        public float FollowDistance => followDistance;
        
        public float Gravity => gravity;
        
        public float Velocity => velocity;
    }
}