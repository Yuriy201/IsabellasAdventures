using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private Slider _globalVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;

        private AudioMixer _playerAudioMixer;
        private float _globalVolume;
        private float _sfxVolume;
        private float _multiplier = 20f;

        private const float MAXVOLUME = 0f;
        private const float MINVOLUME = -80f;

        [Inject]
        private void Inject(AudioMixer audioMixer)
        {
            _playerAudioMixer = audioMixer;
        }

        private void Start()
        {
            Load();

            _globalVolumeSlider.onValueChanged.AddListener(SetGlobalVolume);
            _sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

            DontDestroyOnLoad(gameObject);
        }

        private void SetGlobalVolume(float value)
        {
            _playerAudioMixer.SetFloat("Global", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("globalVolume", value);
        }

        private void SetSFXVolume(float value)
        {
            _playerAudioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("SFXVolume", value);
        }

        public void SetVolume()
        {
            _globalVolume = _globalVolumeSlider.value;
            _sfxVolume = _sfxVolumeSlider.value;

            _playerAudioMixer.SetFloat("Global", Mathf.Lerp(MINVOLUME, MAXVOLUME, _globalVolumeSlider.value));
            _playerAudioMixer.SetFloat("SFX", Mathf.Lerp(MINVOLUME, MAXVOLUME, _sfxVolumeSlider.value));

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
            _globalVolumeSlider.value = PlayerPrefs.GetFloat("globalVolume", 0.5f);
            _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

            SetGlobalVolume(_globalVolumeSlider.value);
            SetSFXVolume(_sfxVolumeSlider.value);
        }
    }
}