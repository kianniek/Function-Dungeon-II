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

        void Start()
        {
            ShowLevel();
        }

        public void ShowLevel()
        {
            showLevelCamera.SetActive(true);
            normalViewCamera.SetActive(false);
            projectileCamera.SetActive(false);
            equationText.SetActive(false);
            fireButton.SetActive(false);
            StartCoroutine(SwitchToCannonView());
        }

        private void CannonView()
        {
            showLevelCamera.SetActive(false);
            normalViewCamera.SetActive(true);
            projectileCamera.SetActive(false);
            equationText.SetActive(true);
            fireButton.SetActive(true); 
        }

        public void ShowProjectile()
        {
            foreach (ProjectileScript projectile in cannon.GetComponentsInChildren<ProjectileScript>())
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