using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Character Stats/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    [Header("¹¥»÷¾àÀë")]
    public float attackRange;

    public float skillRange;

    [Header("¹¥»÷¼ä¸ô")]
    public float coolDown;

    [Header("¹¥»÷¸¡¶¯Çø¼ä")]
    public int minDamage;
    public int maxDamage;

    [Header("±©»÷¼¸ÂÊ&ÉËº¦")]
    public float criticalDamage;
    public float criticalChance;
}
