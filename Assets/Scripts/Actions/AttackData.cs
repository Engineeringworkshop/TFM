using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttackData", menuName = "Data/Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Attack atributes")]
    public float attackDamage; // Damage of each success attack
    public float attackRange;
    public float attacksAmount; // Number of attacks
}
