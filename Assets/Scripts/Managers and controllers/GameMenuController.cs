using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private int firstMapSceneIndex;
    [SerializeField] private BlackPanelController blackPanelController;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject audioSettingMenu;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ButtonMethods buttonMethods;

    [Header("Timers")]
    [SerializeField] private float startFadeInTime;
    [SerializeField] private float delayLevelStartTime;

    // Timers
    private WaitForSecondsRealtime delayLevelStartTimer;

    private void OnValidate()
    {
        if (audioManager == null)
        {
            audioManager.GetComponent<AudioManager>();
        }

        if (buttonMethods == null)
        {
            buttonMethods = GetComponent<ButtonMethods>();
        }
    }

    private void Awake()
    {
        // Timers
        delayLevelStartTimer = new WaitForSecondsRealtime(delayLevelStartTime);

        menuPanel.SetActive(false);
        audioSettingMenu.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(LoadStartFadeIn());
    }

    private IEnumerator LoadStartFadeIn()
    {
        yield return StartCoroutine(blackPanelController.FadeOut(1, 0, startFadeInTime));

        menuPanel.SetActive(true);
    }

    public void NewGame()
    {
        // Reproduce click sound
        audioManager.ReproduceClickSoundOnce();

        StartCoroutine(DelayLevelStart());
    }

    private IEnumerator DelayLevelStart()
    {
        yield return delayLevelStartTimer;

        SceneManager.LoadScene(firstMapSceneIndex);
    }

    public void ToggleAudioSettings()
    {
        bool isActive = audioSettingMenu.activeSelf;

        // Reproduce click sound
        audioManager.ReproduceClickSoundOnce();

        // If audio settings panel is not active: activate it then load stored values.
        if (!isActive)
        {
            // Set the panels
            audioSettingMenu.SetActive(true);
            menuPanel.SetActive(false);

            // Load values on sliders
            audioManager.LoadAudioVolumeValues();
        }
        // If audio settings panel is active: Save values then close it
        else
        {
            // Set the panels
            menuPanel.SetActive(true);
            audioSettingMenu.SetActive(false);
        }
    }

    public void ExitGame()
    {
        // Reproduce click sound
        audioManager.ReproduceClickSoundOnce();

        buttonMethods.ExitAppButton();
    }
}
