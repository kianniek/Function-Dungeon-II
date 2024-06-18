using LinearProjectiles;
using UnityEngine;
using Utils;

namespace Towers.Configuration.UI
{
    /// <summary>
    /// A UI controller for configuring a shooting tower.
    /// </summary>
    public class ShootingConfiguratorUIController : TypedTowerUIController
    {
        private const int LineResolution = 2;

        [Header("Configuration Guide")] 
        [SerializeField] private LineRenderer shootingDirectionGuide;

        private float _a;
        private Vector3 _pointOnShootingCircle;

        /// <summary>
        /// The angle of the shooting direction.
        /// </summary>
        public float A
        {
            private get => _a;
            set
            {
                _a = value;

                if (!ActiveTower)
                    return;

                _pointOnShootingCircle = ActiveTower.transform.position;

                var pointOnIdentity =
                    MathExtensions.CalculatePointOnCircle(MathExtensions.AToRadians(A)) *
                    ActiveTower.TowerVariables.FireRange;

                _pointOnShootingCircle.x -= pointOnIdentity.x;
                _pointOnShootingCircle.z -= pointOnIdentity.y;

                shootingDirectionGuide.positionCount = LineResolution;
                shootingDirectionGuide.SetPositions(new[] { ActiveTower.transform.position, _pointOnShootingCircle });
            }
        }

        /// <summary>
        /// Called when the user confirms the angle.
        /// </summary>
        public override void OnConfirmButtonClicked()
        {
            ActiveTower.GetComponent<LinearProjectileTower>().SetShootingPosition(_pointOnShootingCircle);

            onTowerConfigured?.Invoke();

            gameObject.SetActive(false);
        }
    }
}