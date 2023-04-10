using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]
public class Character_Data : ScriptableObject
{
    [Header("Character atributes")]
    public string characterName;
    public Sprite characterPortrait;
    public bool isLocked;

    [Header("Robot info")]
    public float movementSpeed;
    public float maxHealth;
    public string playerName;
    public float attackRange;
    public float attackDamage;
    [Range(0f, 1f)] public float armor;

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

    [Header("Effects")]
    public ParticleSystem impactEffect;
}
