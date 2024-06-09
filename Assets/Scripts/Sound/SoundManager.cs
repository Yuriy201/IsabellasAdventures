using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private Slider _globalValumeSlider;
        [SerializeField] private Slider _sfxValumeSlider;

        private AudioMixer _playerAudioMixer;
        private float _globalVolume;
        private float _sfxVolume;
        private float _multiplier = 20f;

        private const float MAXVOLUME = 0f;
        private const float MINVOLUME = -80f;

        [Inject]
        private void Inject(AudioMixer playerAudioSource)
        {
            _playerAudioMixer = playerAudioSource;
        }

        private void Start()
        {
            Load();
            DontDestroyOnLoad(gameObject);
        }

        public void SetVolume()
        {
            _globalVolume = _globalValumeSlider.value;
            _sfxVolume = _sfxValumeSlider.value;

            _playerAudioMixer.SetFloat("Global", Mathf.Lerp(MINVOLUME, MAXVOLUME, _globalValumeSlider.value));
            _playerAudioMixer.SetFloat("SFX", Mathf.Lerp(MINVOLUME, MAXVOLUME, _sfxValumeSlider.value));

            StartCoroutine(Save());
        }

        private IEnumerator Save()
        {
            PlayerPrefs.SetFloat("GlobalValue", _globalVolume);
            yield return null;
            PlayerPrefs.SetFloat("SFXValue", _sfxVolume);
        }

        private void Load()
        {
            _globalValumeSlider.value = PlayerPrefs.GetFloat("GlobalValue");
            _sfxValumeSlider.value = PlayerPrefs.GetFloat("SFXValue");

            SetVolume();
        }
    }
}