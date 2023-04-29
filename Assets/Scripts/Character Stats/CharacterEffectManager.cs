using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum CharacterEffectType
{
    Floor,
    HitRightHand
}

public class CharacterEffectManager : MonoBehaviour
{
    [Header("Effect origins")]
    [SerializeField] private Transform floorOrigin;
    [SerializeField] private Transform hitRightHandOrigin;

    [Header("Effect configuration")]
    [SerializeField] private float effectDestructionDelay;

    private List<GameObject> effectGameObjectsList = new List<GameObject>();

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        GameplayManager.OnRoundEnd += DestroyAllEffectsImmediately;
    }

    private void OnDisable()
    {
        GameplayManager.OnRoundEnd -= DestroyAllEffectsImmediately;
    }

    #endregion

    /// <summary>
    /// Method to instantiate effec with the given CharacterEffectData, checks the CharacterEffectType parameter to instantiate the effect to the desired parent to follow (or not) the setted transform.
    /// </summary>
    /// <param name="characterEffectData"></param>
    /// <returns></returns>
    public GameObject InstantiateEffect(CharacterEffectData characterEffectData)
    {
        GameObject effect = null;

        if (characterEffectData.characterEffectType == CharacterEffectType.Floor) 
        {
            effect = InstantiateEffectInPlace(characterEffectData, floorOrigin);
        }
        else if (characterEffectData.characterEffectType == CharacterEffectType.HitRightHand)
        {
            effect = InstantiateEffectInPlace(characterEffectData, hitRightHandOrigin);
        }

        return effect;
    }

    /// <summary>
    /// Method to destroy the given effect
    /// </summary>
    /// <param name="effect"></param>
    public void DestroyEffect(GameObject effect)
    {
        // TODO Pause effect emision

        // Remove effect from the effect list
        effectGameObjectsList.Remove(effect);

        // Destroy effect after 5 seconds
        Destroy(effect, effectDestructionDelay);
    }

    /// <summary>
    /// Method to destroy all effects inmediately
    /// </summary>
    public void DestroyAllEffectsImmediately()
    {
        foreach (var effect in effectGameObjectsList)
        {
            Destroy(effect);
        }

        // Clean the effect list
        effectGameObjectsList.Clear();
    }

    /// <summary>
    /// Method to Instantaite the given effect and attach it to the given parent. The method also add the effect to a list to allow to destroy all effect at the same time.
    /// </summary>
    /// <param name="characterEffectData"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private GameObject InstantiateEffectInPlace(CharacterEffectData characterEffectData, Transform parent)
    {
        GameObject effect;

        effect = Instantiate(characterEffectData.characterEffect, parent);

        effectGameObjectsList.Add(effect);

        return effect;
    }
}
