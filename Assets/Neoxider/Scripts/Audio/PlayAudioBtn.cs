using UnityEngine;
using UnityEngine.UI;

namespace NeoxiderAudio
{
    public class PlayAudioBtn : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

<<<<<<< Updated upstream
        private AudioManager _am;

        private void Start()
        {
            _am = AudioManager.Instance;
        }

=======
>>>>>>> Stashed changes
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
            AudioManager.PlaySound();
        }

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }
    }
}
