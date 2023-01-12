using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject audioSettingMenu;

    [SerializeField] private AudioManager audioManager;

    // Events
    public delegate void OpenPauseMenu();
    public static event OpenPauseMenu OnOpenPauseMenu;

    public delegate void ClosePauseMenu();
    public static event ClosePauseMenu OnClosePauseMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        audioSettingMenu.SetActive(false);
    }

    public void ToggleGameMenu(bool isPaused)
    {
        pauseMenu.SetActive(isPaused);

        // Reproduce click sound
        audioManager.ReproduceClickSoundOnce();

        if (!isPaused)
        {
            audioSettingMenu.SetActive(false);
        }

        // Activate events
        if (isPaused && OnOpenPauseMenu != null)
        {
            OnOpenPauseMenu();
        }
        else if (!isPaused && OnClosePauseMenu != null)
        {
            OnClosePauseMenu();
        }
    }

    public void ToggleAudioSettings()
    {
        bool isActive = audioSettingMenu.activeSelf;

        // Reproduce click sound
        audioManager.ReproduceClickSoundOnce();

        // If audio settings panel is not active: activate it then load stored values.
        if (!isActive)
        {
            audioSettingMenu.SetActive(true);
            audioManager.LoadAudioVolumeValues();
        }
        // If audio settings panel is active: Save values then close it
        else
        {
            audioManager.SaveAudioVolumeValues();
            audioSettingMenu.SetActive(false);
        }   
    }
}
