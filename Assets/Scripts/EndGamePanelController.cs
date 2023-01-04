using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanelObject;
    [SerializeField] private TMP_Text winnerNameText;

    private void Awake()
    {
        winnerNameText.text = string.Empty;

        endGamePanelObject.SetActive(false);
    }

    public void LoadEndGamePanel(string winnerName)
    {
        endGamePanelObject.SetActive(true);
        winnerNameText.text = winnerName;
    }

    #region Buttons methods

    public void PlayAgainButton()
    {
        // Debug
        print("Play again button pulsed");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitAppButton()
    {
        // Debug
        print("Exit app");

        Application.Quit();
    }

    #endregion
}
