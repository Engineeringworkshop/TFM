using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFrame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject characterLockImage;

    [Header("Data")]
    [SerializeField] private Character_Data characterData;

    private void OnValidate()
    {
        if (characterLockImage != null && characterData != null)
        {
            characterLockImage.SetActive(characterData.isLocked);
        }
    }

    private void Awake()
    {
        if (characterData != null)
        {
            characterLockImage.SetActive(characterData.isLocked);
        } 
    }

    public Sprite GetCharacterPortrait()
    {
        if (characterData == null)
        {
            return null;
        }

        if (characterData.isLocked)
        {
            Debug.Log("Character locked");

            //TODO Reproduce character blocked sound

            return null;
        }
        else
        {
            return characterData.characterPortrait;
        }
    }
}
