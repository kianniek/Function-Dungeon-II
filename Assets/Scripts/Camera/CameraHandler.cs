using System.Collections;
using Cinemachine;
using Projectile;
using UnityEngine;

namespace Camera
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private GameObject showLevelCamera;
        [SerializeField] private GameObject normalViewCamera;
        [SerializeField] private GameObject projectileCamera;
        [SerializeField] private GameObject cannon;
        [SerializeField] private GameObject equationText;
        [SerializeField] private GameObject fireButton;
        [SerializeField] private int timeBetweenLevelAndCannonView = 3;

        private CinemachineVirtualCamera _projectileVirtualCamera;

        private void Awake()
        {
            _projectileVirtualCamera = projectileCamera.GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            ShowLevel();
        }

        /// <summary>
        /// Camera view of the whole level, this view shows up first
        /// </summary>
        public void ShowLevel()
        {
            showLevelCamera.SetActive(true);
            normalViewCamera.SetActive(false);
            projectileCamera.SetActive(false);
            equationText.SetActive(false);
            fireButton.SetActive(false);
            StartCoroutine(SwitchToCannonView());
        }

        /// <summary>
        /// Camera view of the cannon, timeBetweenLevelAndCannon view variable decide after how much seconds this camera activates from ShowLevelView
        /// </summary>
        private void CannonView()
        {
            showLevelCamera.SetActive(false);
            normalViewCamera.SetActive(true);
            projectileCamera.SetActive(false);
            equationText.SetActive(true);
            fireButton.SetActive(true); 
        }

        /// <summary>
        /// Camera view of following projectile, shown when pressed on fire. After projectile landed go back to level view
        /// </summary>
        public void ShowProjectile()
        {
            foreach (var projectile in cannon.GetComponentsInChildren<ProjectileScript>())
            {
                if (projectile.isActiveAndEnabled)
                {
                    projectile.ChangeCameraView.RemoveListener(ShowLevel);
                    projectile.ChangeCameraView.AddListener(ShowLevel);
                    _projectileVirtualCamera.Follow = projectile.gameObject.transform;
                }
            }
            showLevelCamera.SetActive(false);
            normalViewCamera.SetActive(false);
            projectileCamera.SetActive(true);
            equationText.SetActive(false);
            fireButton.SetActive(false);
        }

        private IEnumerator SwitchToCannonView()
        {
            yield return new WaitForSeconds(timeBetweenLevelAndCannonView);
            CannonView();
        }
    }
}