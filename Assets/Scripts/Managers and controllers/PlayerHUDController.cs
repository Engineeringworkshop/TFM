using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text nameText;

    [SerializeField] private Image winCounter1;
    [SerializeField] private Image winCounter2;

    [SerializeField] private Sprite winSprite;
    [SerializeField] private Sprite emptySprite;

    public void SetMaxHealthValue(float value)
    {
        slider.maxValue = value;
    }

    public void SetHealthBarValue(float value)
    {
        slider.value = value;
    }

    public void SetPlayerName(string name)
    {
        nameText.text = name;
    }

    public void UpdateWinCounter(int winCount)
    {
        if (winCount == 0)
        {
            winCounter1.sprite = emptySprite;
            winCounter2.sprite = emptySprite;
        }
        if (winCount == 1)
        {
            winCounter1.sprite = winSprite;
            winCounter2.sprite = emptySprite;
        }
        else if (winCount == 2)
        {
            winCounter1.sprite = winSprite;
            winCounter2.sprite = winSprite;
        }
    }
}
