using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio clips")]
    [SerializeField] private AudioClip clickAudio;

    public void ReproduceClickSound()
    {
        audioSource.PlayOneShot(clickAudio);
    }
}
