using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private PlayerSelectionController player1;
    [SerializeField] private PlayerSelectionController player2;

    [Header("References")]
    [SerializeField] private MapSelectorController mapSelectorController;

    [Header("Sound")]
    [SerializeField] private AudioClip playersNotReadyAudio;

    private AudioSource audioSource;

    private void OnValidate()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (mapSelectorController == null)
        {
            mapSelectorController = GetComponent<MapSelectorController>();
        }
    }

    public void StartFight()
    {
        // If both players are ready load map
        if (player1.IsPlayerReady && player2.IsPlayerReady)
        {
            // Load map
            mapSelectorController.LoadSelectedScene();
        }
        // If not reproduce sound "¡Players not ready!
        else
        {
            audioSource.PlayOneShot(playersNotReadyAudio);
        }
    }
}
