using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private PlayerSelectionController player1;
    [SerializeField] private PlayerSelectionController player2;

    [Header("References")]
    [SerializeField] private MapSelectorController mapSelectorController;

    [Header("Sound")]
    [SerializeField] private AudioClip playersReadyAudio;
    [SerializeField] private AudioClip playersNotReadyAudio;

    [Header("Debug")]
    [SerializeField] private AudioSource audioSource;

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
            StartCoroutine(LoadSelectedScene());
        }
        // If not reproduce sound "¡Players not ready!
        else
        {
            audioSource.PlayOneShot(playersNotReadyAudio);
        }
    }

    private IEnumerator LoadSelectedScene()
    {
        audioSource.PlayOneShot(playersReadyAudio);

        yield return new WaitForSecondsRealtime(playersReadyAudio.length);
        
        mapSelectorController.LoadSelectedScene();
    }

    public void ReturnToGameMenu()
    {
        StartCoroutine(ReturnToGameMenuCoroutine());
    }

    private IEnumerator ReturnToGameMenuCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene(0);
    }
}
