using modules.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public CharacterStat Strength;
    [SerializeField] public CharacterStat Agility;

    [Header("References")]
    [SerializeField] private CharacterEffectManager characterEffectManager;

    private List<StatModifierData> statModifierDataList = new List<StatModifierData>();

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        GameplayManager.OnRoundEnd += RemoveAllModifiersImmediately;
    }

    private void OnDisable()
    {
        GameplayManager.OnRoundEnd -= RemoveAllModifiersImmediately;
    }

    #endregion

    /// <summary>
    /// Method to add the given StatModifierData to the 
    /// </summary>
    /// <param name="statModifierData"></param>
    public void AddPowerUp(StatModifierData statModifierData)
    {
        GameObject effect = null;

        // Add the modifier to the stat
        statModifierData.AddPowerUp(this);

        // Instantiate effect
        if (statModifierData.characterEffectData != null)
        {
            effect = characterEffectManager.InstantiateEffect(statModifierData.characterEffectData);
        }

        // Add the effect to the list
        statModifierDataList.Add(statModifierData);

        // Set coroutine to remove the modifier when the time end
        StartCoroutine(RemovePowerUpTimer(statModifierData, effect));

        Debug.Log("Strenght: " + Strength.Value + " Agility: " + Agility.Value);
    }

    /// <summary>
    /// Method to remove all the stat modifiers immediately
    /// </summary>
    private void RemoveAllModifiersImmediately()
    {
        // Create an auxiliar copy of the list to aviod error of modified list while is removing the stats
        var auxList = statModifierDataList.ToList();

        foreach (var statModifierData in auxList)
        {
            RemovePowerUp(statModifierData);
        }

        statModifierDataList.Clear();
    }

    private IEnumerator RemovePowerUpTimer(StatModifierData statModifierData, GameObject effect)
    {
        yield return new WaitForSecondsRealtime(statModifierData.statModifierTime);

        RemovePowerUp(statModifierData);

        if (effect != null)
        {
            characterEffectManager.DestroyEffect(effect);
        }
    }

    /// <summary>
    /// Method to remove the given StatModifierData from the CharacterEffectManager
    /// </summary>
    /// <param name="statModifierData"></param>
    private void RemovePowerUp(StatModifierData statModifierData)
    {
        statModifierData.RemovePowerUp(this);

        // Add the effect to the list
        statModifierDataList.Remove(statModifierData);

        Debug.Log("Strenght: " + Strength.Value + " Agility: " + Agility.Value);
    }
}
