using modules.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStatModifierData", menuName = "Data/Stat Modifier Data")]
public class StatModifierData : ScriptableObject
{
    [Header("Stat modifier atributes")]
    public int StrengthBonus;
    public int AgilityBonus;
    [Space]
    public float StrengthPercentBonus;
    public float AgilityPercentBonus;

    public void AddPowerUp(RobotStatsManager robotStatsManager)
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

    public void RemovePowerUp(RobotStatsManager robotStatsManager)
    {
        robotStatsManager.Strength.RemoveAllModifiersFromSource(this);
        robotStatsManager.Agility.RemoveAllModifiersFromSource(this);
    }
}
