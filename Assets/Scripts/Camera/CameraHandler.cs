using System.Collections;
using Events.GameEvents;
using Events.GameEvents.Typed;
using Unity.Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraHandler : MonoBehaviour
    {
        private const int LowPriority = 10;
        private const int HighPriority = 20;
        
        [SerializeField] private CinemachineVirtualCamera showLevelCamera;
        [SerializeField] private CinemachineVirtualCamera normalViewCamera;
        [SerializeField] private CinemachineVirtualCamera projectileCamera;
        [SerializeField] private int timeBetweenLevelAndNormalView = 3;

        [SerializeField] private GameEvent onAmmoDepleted;
        [SerializeField] private GameObjectGameEvent onCameraFollowGameObject;
        
        private bool _ammoDepleted;
        
        private void Awake()
        {
            projectileCamera.Priority.Value = LowPriority;
            showLevelCamera.Priority.Value = LowPriority;
            normalViewCamera.Priority.Value = LowPriority;
        }

        private void OnEnable()
        {
            onAmmoDepleted.AddListener(SetAmmoDepleted);
            onCameraFollowGameObject.AddListener(FollowGameObject);
        }

        private void OnDisable()
        {
            onAmmoDepleted.RemoveListener(SetAmmoDepleted);
            onCameraFollowGameObject.RemoveListener(FollowGameObject);
        }

        private void Start()
        {
            ShowLevel();
        }

        private void SetAmmoDepleted()
        {
            _ammoDepleted = true;
        }

        /// <summary>
        /// Camera view of the whole level, this view shows up first
        /// </summary>
        public void ShowLevel()
        {
            showLevelCamera.Priority.Value = HighPriority;

            if (_ammoDepleted)
                return;

            StartCoroutine(SwitchToCannonView());
        }

        /// <summary>
        /// Camera view of the cannon, timeBetweenLevelAndCannon view variable decide after how much seconds this camera activates from ShowLevelView
        /// </summary>
        private void CannonView()
        {
            normalViewCamera.Priority.Value = HighPriority;
        }

        /// <summary>
        /// Camera view of following projectile, shown when pressed on fire. After projectile landed go back to level view
        /// </summary>
        public void FollowGameObject(GameObject objectToFollow)
        {
            normalViewCamera.Priority.Value = LowPriority;

            // If the object to follow is null, then we should go back to the level view
            if (!objectToFollow)
            {
                projectileCamera.Priority.Value = LowPriority;
                projectileCamera.Follow = null;
                
                ShowLevel();
                
                return;
            }

            // Otherwise, we should follow the object
            projectileCamera.Priority.Value = HighPriority;
            projectileCamera.Follow = objectToFollow.transform;
        }

        private IEnumerator SwitchToCannonView()
        {
            yield return new WaitForSeconds(timeBetweenLevelAndNormalView);
            
            CannonView();
        }
    }
}