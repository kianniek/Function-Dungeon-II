using System.Collections;
using Cinemachine;
using Events.GameEvents;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Camera
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera showLevelCamera;
        [SerializeField] private CinemachineVirtualCamera normalViewCamera;
        [SerializeField] private CinemachineVirtualCamera projectileCamera;
        [SerializeField] private int timeBetweenLevelAndNormalView = 3;
        
        [SerializeField] private GameEvent onAmmoDepleted;
        [SerializeField] private GameObjectGameEvent onCameraFollowGameObject;
        private bool _ammoDepleted;

        private const int LowPriority = 10;
        private const int HighPriority = 20;

        private void Awake()
        {
            projectileCamera.Priority = LowPriority;
            showLevelCamera.Priority = LowPriority;
            normalViewCamera.Priority = LowPriority;
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
            showLevelCamera.Priority = HighPriority;
            
            if (_ammoDepleted)
                return;
            
            StartCoroutine(SwitchToCannonView());
        }

        /// <summary>
        /// Camera view of the cannon, timeBetweenLevelAndCannon view variable decide after how much seconds this camera activates from ShowLevelView
        /// </summary>
        private void CannonView()
        {
            normalViewCamera.Priority = HighPriority;
        }

        /// <summary>
        /// Camera view of following projectile, shown when pressed on fire. After projectile landed go back to level view
        /// </summary>
        public void FollowGameObject(GameObject objectToFollow)
        {
            normalViewCamera.Priority = LowPriority;

            // If the object to follow is null, then we should go back to the level view
            if(objectToFollow == null)
            {
                projectileCamera.Priority = LowPriority;
                projectileCamera.Follow = null;
                ShowLevel();
                return;
            }

            // Otherwise, we should follow the object
            projectileCamera.Priority = HighPriority;
            projectileCamera.Follow = objectToFollow.transform;
        }

        private IEnumerator SwitchToCannonView()
        {
            yield return new WaitForSeconds(timeBetweenLevelAndNormalView);
            CannonView();
        }
    }
}