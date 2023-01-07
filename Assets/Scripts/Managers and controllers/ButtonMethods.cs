using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour
{
    #region Buttons methods

    static public void ResumeGame()
    {

    }

    static public void RestartGameButton()
    {
        // Debug
        Debug.Log("Restart game");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    static public void ExitAppButton()
    {
        // Debug
        Debug.Log("Exit app");

        Application.Quit();
    }

    #endregion
}
