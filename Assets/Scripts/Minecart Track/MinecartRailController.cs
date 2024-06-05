using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cannon;
using Events;
using Events.GameEvents.Typed;
using UnityEngine;

namespace MinecartTrack
{
    public class MinecartRailController : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent onTrackPlaced;
        [SerializeField] private GameObjectGameEvent onTrackConfirmPlacement;
        [SerializeField] private FloatEvent changeSlope = new();
        [SerializeField]  private FloatEvent changeHeight = new();

        [SerializeField] private List<MinecartTrack> _minecartTracks = new List<MinecartTrack>();
        [SerializeField] private MinecartTrack _currentTrack;


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
            RemoveCurentTrack();
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

        public void OnChangeSlope(float angle)
        {
            changeSlope.Invoke(angle);
        }

        public void OnChangeHeight(float heigth)
        {
            changeHeight.Invoke(heigth);
        }
    }
}