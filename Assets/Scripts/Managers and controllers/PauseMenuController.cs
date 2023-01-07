using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;

    private void Awake()
    {
        gameMenu.SetActive(false);
    }

    public void ToggleGameMenu(bool isPaused)
    {
        gameMenu.SetActive(isPaused);
    }
}
