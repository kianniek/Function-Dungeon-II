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
        private FloatEvent changeSlope = new();
        private FloatEvent changeHeight = new();

        private List<MinecartTrack> _minecartTracks = new List<MinecartTrack>();
        private MinecartTrack _currentTrack;


        private void OnEnable()
        {
            onTrackPlaced.AddListener(ChangeCurrentTrack);
        }

        private void OnDisable()
        {
            onTrackPlaced.RemoveListener(ChangeCurrentTrack);
        }

        private void ChangeCurrentTrack(GameObject track)
        {
            if (_currentTrack != null)
            {
                changeSlope.RemoveAllListeners();
                changeHeight.RemoveAllListeners();
            }

            _currentTrack = track.GetComponent<MinecartTrack>();

            changeSlope.AddListener(_currentTrack.GetComponent<LerpedYTranslation>().Move);
            //changeHeight.AddListener(_currentTrack.GetComponent<ObjectSlopeAngleController>().Slope);
            _minecartTracks.Add(_currentTrack);
        }

        public void OnChangeSlope(float angle)
        {
            changeSlope.Invoke(angle);
        }

        public void OnChangeHeight(float heigth)
        {

        }
    }
}