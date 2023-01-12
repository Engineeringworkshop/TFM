using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    [SerializeField] private AudioMixerSnapshot pauseAudioSnapshot;
    [SerializeField] private AudioMixerSnapshot unpauseAudioSnapshot;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider interfaceVolumeSlider;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;

    private void OnValidate()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        PauseMenuController.OnOpenPauseMenu += SetPauseAudioSnapshot;
        PauseMenuController.OnClosePauseMenu += SetUnpauseAudioSnapshot;
    }

    private void OnDisable()
    {
        PauseMenuController.OnOpenPauseMenu -= SetPauseAudioSnapshot;
        PauseMenuController.OnClosePauseMenu -= SetUnpauseAudioSnapshot;
    }

    private void Awake()
    {
        
    }

    private void SetPauseAudioSnapshot()
    {
        pauseAudioSnapshot.TransitionTo(0.01f);
    }

    private void SetUnpauseAudioSnapshot()
    {
        unpauseAudioSnapshot.TransitionTo(0.01f);
    }

    /// <summary>
    /// Update slider value from player prefs saved volume values. Call it when the audio menu configuration opens.
    /// </summary>
    public void LoadAudioVolumeValues()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0f);
        interfaceVolumeSlider.value = PlayerPrefs.GetFloat("InterfaceVolume", 0f);
    }

    public void SaveMasterVolumeSliderValue()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);

        UpdateMixerVolumeValues();
    }

    public void SaveMusicVolumeSliderValue()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);

        UpdateMixerVolumeValues();
    }

    public void SaveEffectsVolumeSliderValue()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);

        UpdateMixerVolumeValues();
    }

    public void SaveInterfaceVolumeSliderValue()
    {
        PlayerPrefs.SetFloat("InterfaceVolume", interfaceVolumeSlider.value);

        UpdateMixerVolumeValues();
    }

    /// <summary>
    /// Update the volume values on player prefs. Call it on volume slider change value.
    /// </summary>
    public void SaveAudioVolumeValues()
    {
        SaveMasterVolumeSliderValue();
        SaveMusicVolumeSliderValue();
        SaveEffectsVolumeSliderValue();
        SaveInterfaceVolumeSliderValue();

        UpdateMixerVolumeValues();
    }

    /// <summary>
    /// Update audio mixer volume values when the user change the slider value.
    /// </summary>
    private void UpdateMixerVolumeValues()
    {
        masterMixer.SetFloat("MasterVolume", masterVolumeSlider.value);
        masterMixer.SetFloat("MusicVolume", musicVolumeSlider.value);
        masterMixer.SetFloat("EffectsVolume", effectsVolumeSlider.value);
        masterMixer.SetFloat("InterfaceVolume", interfaceVolumeSlider.value);
    }

    public void ReproduceClickSoundOnce()
    {
        //audioSource.Stop();
        audioSource.PlayOneShot(clickSound);
    }
}
