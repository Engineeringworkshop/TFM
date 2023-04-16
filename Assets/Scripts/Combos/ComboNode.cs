using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newComboNodeData", menuName = "Data/Combo Data")]
public class ComboNode : ScriptableObject
{
    [Header("Node atributes")]
    public Sprite targetSprite;
    public List<ComboNode> subComboNodeList;
    public ComboNodeType nodeType;
    public string animation; // Name of the animation of this node

    [Header("Attack stats")]
    public float attackDamage; // Damage of each success attack
    public float attacksAmount; // Number of attacks
    public float attackRange;
}
