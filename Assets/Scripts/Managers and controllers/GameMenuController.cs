using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private int firstMapSceneIndex;

    public void NewGame()
    {
        SceneManager.LoadScene(firstMapSceneIndex);
    }

    public void OptionsMenu()
    {
        Debug.Log("Open options menu");
    }

    public void ExitGame()
    {
        ButtonMethods.ExitAppButton();
    }
}
