using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Character Stats/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    [Header("��������")]
    public float attackRange;

    public float skillRange;

    [Header("�������")]
    public float coolDown;

    [Header("������������")]
    public int minDamage;
    public int maxDamage;

    [Header("��������&�˺�")]
    public float criticalDamage;
    public float criticalChance;
}
