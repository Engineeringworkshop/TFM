using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCharacterEffectData", menuName = "Data/Character Effect Data")]
public class CharacterEffectData : ScriptableObject
{
    [Header("")]
    public GameObject characterEffect;
    public CharacterEffectType characterEffectType;
}
