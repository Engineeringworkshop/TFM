using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSelectionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text readyButtonText;
    [SerializeField] private Image playerSelectedCharacterPortrait;

    [Header("Configuration")]
    [SerializeField] public Character_Data defaultCharacterSelected;

    [Header("Debug")]
    [SerializeField] public Character_Data currentCharacterSelected;

    [SerializeField] public bool IsPlayerReady { get; private set; }

    private void OnValidate()
    {
        if (defaultCharacterSelected != null)
        {
            playerSelectedCharacterPortrait.sprite = defaultCharacterSelected.characterPortrait;
        }
    }

    private void Awake()
    {
        readyButtonText.text = "Not ready!";

        currentCharacterSelected = defaultCharacterSelected;

        UpdatePlayerPortrait();
    }

    public void ToggleReady()
    {
        IsPlayerReady = !IsPlayerReady;

        if (IsPlayerReady)
        {
            readyButtonText.text = "Ready!";
        }
        else
        {
            readyButtonText.text = "Not ready!";
        }
    }

    public void UpdatePlayerPortrait()
    {
        playerSelectedCharacterPortrait.sprite = currentCharacterSelected.characterPortrait;
    }
}
