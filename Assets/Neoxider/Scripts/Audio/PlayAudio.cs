using UnityEngine;

namespace NeoxiderAudio
{
    public class PlayAudio : MonoBehaviour
    {
        [SerializeField]
        private ClipType _clipType;

        [SerializeField]
        private float _volume = 1;

        public void AudioPlay()
        {
            AudioManager.PlaySound(_clipType);
        }

        private void OnValidate()
        {
            name = nameof(PlayAudio) + " " + _clipType.ToString();
        }
    }
}
