using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameplayManager gameplayManager;

    private void OnValidate()
    {
        if (audioManager == null)
        {
            audioManager = GetComponent<AudioManager>();
        }

        if (gameplayManager == null)
        {
            gameplayManager = GetComponent<GameplayManager>();
        }
    }

    #region Buttons methods

    public void ResumeGame()
    {
        // Reproduce click sound
        audioManager.ReproduceClickSoundOnce();

        gameplayManager.TogglePauseGame();
    }

    public void RestartGameButton()
    {
        // Debug
        //Debug.Log("Restart game");

        // Reproduce click sound
        audioManager.ReproduceClickSoundOnce();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitAppButton()
    {
        // Debug
        Debug.Log("Exit app");

        // Reproduce click sound
        audioManager.ReproduceClickSoundOnce();

        Application.Quit();
    }

    #endregion
}
