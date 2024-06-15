using System.Collections.Generic;
using Cannon;
using Events;
using Events.GameEvents.Typed;
using UnityEngine;
using UnityEngine.Events;

namespace Minecart
{
    public class MinecartRailController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float trackPlacementCompletionX;

        [Header("Events")]
        // TODO: Change these gameobjectevents to new minecartRail events
        [SerializeField] private GameObjectGameEvent onTrackPlaced;
        [SerializeField] private GameObjectGameEvent onTrackConfirmPlacement;

        [SerializeField] private FloatEvent changeSlope = new();
        [SerializeField] private FloatEvent changeHeight = new();
        [SerializeField] private UnityEvent trackCompleted = new();

        [Header("References")]
        [SerializeField] private MinecartTrack firstTrack;

        private List<MinecartTrack> _minecartTracks = new List<MinecartTrack>();
        [SerializeField] private Dictionary<Vector2, int> _connectionPoints = new Dictionary<Vector2, int>();
        private MinecartTrack _currentTrack;

        private void Start()
        {
            AddFirstTrack();
        }

        private void AddFirstTrack()
        {
            _connectionPoints.Add(_currentTrack.RightConnectionPoint, 1);
        }

        private void OnEnable()
        {
            onTrackPlaced.AddListener(ChangeCurrentTrack);
            onTrackConfirmPlacement.AddListener(ConfirmTrackPlacement);
        }

        private void OnDisable()
        {
            onTrackPlaced.RemoveListener(ChangeCurrentTrack);
            onTrackConfirmPlacement.RemoveListener(ConfirmTrackPlacement);
        }

        private void ChangeCurrentTrack(GameObject track)
        {
            RemoveCurentTrack();

            _currentTrack = track.GetComponent<MinecartTrack>();

            changeHeight.AddListener(_currentTrack.GetComponent<LerpedYTranslation>().Move);
            changeSlope.AddListener(_currentTrack.GetComponent<ObjectSlopeAngleController>().SetSlope);

            _minecartTracks.Add(_currentTrack);
        }

        private void ConfirmTrackPlacement()
        {
            _currentTrack.SetConnectionPoint();

            AddTrackToList();
            RemoveCurentTrack();

            if (IsEveryTrackConnected())
            {
                trackCompleted.Invoke();
            }
        }

        private void AddTrackToList()
        {
            var leftSide = _currentTrack.LeftConnectionPoint;
            var rightSide = _currentTrack.RightConnectionPoint;

            if (_connectionPoints.ContainsKey(leftSide))
                _connectionPoints[leftSide]++;
            else
                _connectionPoints.Add(_currentTrack.LeftConnectionPoint, 1);

            if (_currentTrack.gameObject.transform.position.x < trackPlacementCompletionX)
                if (_connectionPoints.ContainsKey(rightSide))
                    _connectionPoints[rightSide]++;
                else
                    _connectionPoints.Add(_currentTrack.RightConnectionPoint, 1);
        }

        private void RemoveCurentTrack()
        {
            if (_currentTrack != null)
            {
                changeSlope.RemoveAllListeners();
                changeHeight.RemoveAllListeners();
                _currentTrack = null;
            }
        }

        private bool IsEveryTrackConnected()
        {
            foreach (var point in _connectionPoints.Values)
            {
                Debug.Log(point);
                if (point < 2)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Gets called whenever the slope changes of the track
        /// </summary>
        /// <param name="angle">The angle of the change</param>
        public void OnChangeSlope(float angle)
        {
            changeSlope.Invoke(angle);
        }

        /// <summary>
        /// Gets called whenever the slope changes of the track
        /// </summary>
        /// <param name="angle">The H of the change</param>
        public void OnChangeHeight(float height)
        {
            changeHeight.Invoke(height);
        }
    }
}