using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newComboNodeData", menuName = "Data/Combo Data")]
public class Target : ScriptableObject
{
    [Header("Character atributes")]
    public Sprite targetSprite;
}
