using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SettingsManager : MonoBehaviour
{
    private GameConfig gameConfig;

    [Header("Mobile Controls")]
    public Button joystickControlButton;
    public Button buttonsControlButton;

    private MobileControls mobileControls;

    public bool forceMobileControls = false;

    [Header("Audio")]
    public Slider globalVolumeSlider;
    public Slider sfxVolumeSlider;

    private float globalVolume;
    private float sfxVolume;

    private AudioMixer audioMixer;

    [Inject]
    private void Inject(MobileControls mobileControls, AudioMixer audioMixer, GameConfig config)
    {
        this.mobileControls = mobileControls;
        this.audioMixer = audioMixer;
        this.gameConfig = config;
    }

    private void OnValidate()
    {
        if (globalVolumeSlider != null)
        {
            globalVolumeSlider.maxValue = 1f;
            globalVolumeSlider.minValue = 0.0001f;
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.maxValue = 1f;
            sfxVolumeSlider.minValue = 0.0001f;
        }
    }

    private void Awake()
    {
        if (gameConfig.PlatfotmType == PlatfotmType.PC)
        {
            mobileControls.MobileControlsVisibility(false);
            return;
        }
        else
        {
            mobileControls.MobileControlsVisibility(true);
            mobileControls.SwitchControls(ControlType.Buttons);

            joystickControlButton.onClick.AddListener(() => mobileControls.SwitchControls(ControlType.Joystick));
            buttonsControlButton.onClick.AddListener(() => mobileControls.SwitchControls(ControlType.Buttons));
        }

        Load();

        globalVolumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetGlobalVolume(float value)
    {
        globalVolume = value;
        audioMixer.SetFloat("Global", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("globalVolume", value);
    }

    private void SetSFXVolume(float value)
    {
        sfxVolume = value;
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetVolume()
    {
        audioMixer.SetFloat("Global", Mathf.Log10(globalVolume) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);

        StartCoroutine(Save());
    }

    private IEnumerator Save()
    {
        PlayerPrefs.SetFloat("GlobalValue", globalVolume);
        yield return null;
        PlayerPrefs.SetFloat("SFXValue", sfxVolume);
    }

    private void Load()
    {
        globalVolumeSlider.value = PlayerPrefs.GetFloat("globalVolume", 0.5f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        SetGlobalVolume(globalVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
    }
}
