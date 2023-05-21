using modules.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStatModifierData", menuName = "Data/Stat Modifier Data")]
public class StatModifierData : ScriptableObject
{
    [Header("Stat modifier configuration")]
    public float statModifierTime;
    public CharacterEffectData characterEffectData;
    [Space]

    [Header("Stat modifier atributes")]
    public int StrengthBonus;
    public int AgilityBonus;
    [Space]

    public float StrengthPercentBonus;
    public float AgilityPercentBonus;

    /// <summary>
    /// Method to add this stat modifier (Power up) to the character stat in the given RobotStatsManager
    /// </summary>
    /// <param name="robotStatsManager"></param>
    public void AddPowerUp(CharacterStatsManager robotStatsManager)
    {
        // Flat modifiers
        if (StrengthBonus != 0)
            robotStatsManager.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (AgilityBonus != 0)
            robotStatsManager.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));

        // PercentMult mmodifiers
        if (StrengthPercentBonus != 0)
            robotStatsManager.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (AgilityPercentBonus != 0)
            robotStatsManager.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
    }

    /// <summary>
    /// Method to remove this stat modifier (Power up) to the character stat from the given RobotStatsManager
    /// </summary>
    /// <param name="robotStatsManager"></param>
    public void RemovePowerUp(CharacterStatsManager robotStatsManager)
    {
        robotStatsManager.Strength.RemoveAllModifiersFromSource(this);
        robotStatsManager.Agility.RemoveAllModifiersFromSource(this);
    }
}
