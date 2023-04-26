using modules.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStatsManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public CharacterStat Strength;
    [SerializeField] public CharacterStat Agility;

    public void AddPowerUp(StatModifierData statModifierData)
    {
        statModifierData.AddPowerUp(this);

        StartCoroutine(RemovePowerUpTimer(15, statModifierData));

        Debug.Log("Strenght: " + Strength.Value + " Agility: " + Agility.Value);
    }

    private IEnumerator RemovePowerUpTimer(float time, StatModifierData statModifierData)
    {
        yield return new WaitForSecondsRealtime(time);

        RemovePowerUp(statModifierData);
    }

    private void RemovePowerUp(StatModifierData statModifierData)
    {
        statModifierData.RemovePowerUp(this);

        Debug.Log("Strenght: " + Strength.Value + " Agility: " + Agility.Value);
    }
}
