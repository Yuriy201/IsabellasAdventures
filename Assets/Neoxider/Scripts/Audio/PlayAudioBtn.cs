using UnityEngine;
using UnityEngine.UI;

namespace NeoxiderAudio
{
    public class PlayAudioBtn : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private AudioManager _am;

        private void Start()
        {
            _am = AudioManager.Instance;
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
        }
    }
}
