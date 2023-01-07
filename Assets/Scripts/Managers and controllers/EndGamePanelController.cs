using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
