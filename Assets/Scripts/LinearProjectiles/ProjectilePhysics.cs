using UnityEngine;

namespace LinearProjectiles
{
    public static class ProjectilePhysics
    {
        /// <summary>
        /// Calculates the amount of distance the projectile has to travel
        /// </summary>
        /// <param name="direction"> The direction the projectile is moving </param>
        /// <param name="travelDistance"> Radius of the endpoint circle </param>
        /// <returns> The endpoint of the projectile </returns>
        public static Vector2 CalculateGraphTrajectoryEndPoint(Vector2 direction, float travelDistance)
        {
            // Make sure it never returns a zero vector
            if (travelDistance == 0)
                return direction;
            
            return direction.normalized * travelDistance;
        }
        
        public static Vector2 CalculateGravityTrajectory(Vector2 direction, float speed, float gravity)
        {
            return direction.normalized * speed;
        }
    }
}