using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]
public class Robot_Data : ScriptableObject
{
    [Header("Robot info")]
    public float movementSpeed;
    public float maxHealth;
    public string playerName;
    public float attackRange;
    public float attackDamage;
    [Range(0f, 0.8f)] public float armor;

    [Header("Controls")]
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode attackKey;
    public KeyCode defenseKey;

    [Header("Sounds")]
    public AudioClip walkSound;
    public AudioClip attackNoImpactSound;
    public AudioClip impactNotDefendedSound;
    public AudioClip impactDefendedSound;
    public AudioClip takeDamageSound;
}
