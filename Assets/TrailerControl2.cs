using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerControl2 : MonoBehaviour
{
    public CharacterEffectData characterEffectData;

    private CharacterEffectManager characterEffectManager;

    private void OnValidate()
    {
        if (characterEffectData != null)
        {
            characterEffectManager = GetComponent<CharacterEffectManager>();
        }
    }

    private void Start()
    {
        StartCoroutine(WaitAnimationToFinish(null));
    }

    public IEnumerator WaitAnimationToFinish(Robot_State nextState)
    {
        // Wait for the next frame to get the animator info (to wait the next animator state to load)
        yield return new WaitForSeconds(5f);

        characterEffectManager.InstantiateEffect(characterEffectData);
    }
}
