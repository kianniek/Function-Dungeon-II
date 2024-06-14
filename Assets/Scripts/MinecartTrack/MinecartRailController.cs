using System.Collections.Generic;
using Events;
using Events.GameEvents.Typed;
using ObjectMovement;
using UnityEngine;

namespace MineCartTrack
{
    public class MineCartRailController : MonoBehaviour
    {
        [Header("Events")]
        // TODO: Change these gameobjectevents to new minecartRail events
        [SerializeField] private GameObjectGameEvent onTrackPlaced;
        [SerializeField] private GameObjectGameEvent onTrackConfirmPlacement;

        [SerializeField] private FloatEvent changeSlope = new();
        [SerializeField] private FloatEvent changeHeight = new();

        private List<MineCartTrack> _minecartTracks = new List<MineCartTrack>();
        private MineCartTrack _currentTrack;

        private void OnEnable()
        {
            onTrackPlaced.AddListener(ChangeCurrentTrack);
            onTrackConfirmPlacement.AddListener(RemoveCurentTrack);
        }

        private void OnDisable()
        {
            onTrackPlaced.RemoveListener(ChangeCurrentTrack);
            onTrackConfirmPlacement.RemoveListener(RemoveCurentTrack);
        }

        private void ChangeCurrentTrack(GameObject track)
        {
            RemoveCurentTrack();

            _currentTrack = track.GetComponent<MineCartTrack>();

            //changeHeight.AddListener(_currentTrack.GetComponent<LerpedVector2Translation>().Move);
            changeSlope.AddListener(_currentTrack.GetComponent<ObjectSlopeAngleController>().Rotate);

            _minecartTracks.Add(_currentTrack);
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