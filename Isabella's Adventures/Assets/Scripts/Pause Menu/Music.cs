using UnityEngine;
using UnityEngine.UI;

namespace Pause_Menu
{
    public class Music : MonoBehaviour
    {
        private const string MusicVolume = "MusicVolume";
        private const string MusicEnabled = "MusicEnabled";
        [SerializeField] private Slider _slider;
        [SerializeField] private Toggle _isOffSound;
        [SerializeField] private AudioSource _audio;

        private float _volume;
        private int _defaultValue = 1;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(MusicVolume))
            {
                _slider.value = _audio.volume = 1;
            }
            else
            {
                _slider.value = PlayerPrefs.GetFloat(MusicVolume, _defaultValue);
                _isOffSound.isOn = PlayerPrefs.GetInt(MusicEnabled,1) == 1;
            }
        }

        private void Update()
        {
            if (_audio.volume != _slider.value)
            {
                PlayerPrefs.SetFloat(MusicVolume, _slider.value);
                PlayerPrefs.Save();
                _audio.volume = _slider.value;
            }
            
            PlayerPrefs.SetInt(MusicEnabled, enabled ? 1 : 0);
        }
        
        public void ToggleMusic(bool enabled)
        {
            if (AudioListener.volume == 1)
            {
                AudioListener.volume = 0;
                _isOffSound.isOn = false;
            }
            else
            {
                AudioListener.volume = 1;
                _isOffSound.isOn = true;
            }
            
            PlayerPrefs.SetInt(MusicEnabled, enabled ? 1 : 0);
        }
    }
}