using UnityEngine;
using UnityEngine.UI;

namespace NeoxiderAudio
{
    public class PlayAudioBtn : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private AudioManager _am;

        private void Start()
        {

        }

        private void OnEnable()
        {
            _button.onClick.AddListener(AudioPlay);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(AudioPlay);
        }

        private void AudioPlay()
        {
            if (_am != null)
            {
                _am.PlayClick();
            }
        }

        private void OnValidate()
        {
            _button = GetComponent<Button>();
            _am = FindFirstObjectByType<AudioManager>();
        }
    }
}
