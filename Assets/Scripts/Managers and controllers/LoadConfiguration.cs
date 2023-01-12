using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadConfiguration : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    void Start()
    {
        LoadAudioMixerVolumeValues();
    }

    /// <summary>
    /// Load audio mixer volume values from player prefs (use it to load the audio level configuration at the begining of the scene.
    /// </summary>
    private void LoadAudioMixerVolumeValues()
    {
        masterMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 0f));
        masterMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0f));
        masterMixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectsVolume", 0f));
        masterMixer.SetFloat("InterfaceVolume", PlayerPrefs.GetFloat("InterfaceVolume", 0f));
    }
}
