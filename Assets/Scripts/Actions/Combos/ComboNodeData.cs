using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newComboNodeData", menuName = "Data/Combo Data")]
public class ComboNodeData : ScriptableObject
{
    [Header("Node atributes")]
    public Sprite targetSprite;
    public List<ComboNodeData> subComboNodeList;
    public ComboNodeType nodeType;
    public string animation; // Name of the animation of this node

    [Header("Attack stats")]
    public AttackData attackData;

    [Header("Stat modifier")]
    public List<StatModifierData> statModifierData;
}
