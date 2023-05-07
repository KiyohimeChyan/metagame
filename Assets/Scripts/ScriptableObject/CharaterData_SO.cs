using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Deffence Data")]
public class CharaterData_SO : ScriptableObject
{
    [Header("������������")]
    public int maxHealth;
    public int currentHealth;
    public int baseDeffence;
    public int currentDeffence;
    public float invincibleTime;

    [Header("���ﾭ��")]
    public int point;

    [Header("��������")]
    public float currentPower;
    public float maxPower;
    public float powerRecoverSpeed;

    //[Header("����������")]
    //public int currentLevel;
    //public int maxLevel;
    //public int baseEXP;
    //public int currentEXP;
    //public float levelBuff;
    //public float baseEXPBuff;

    //public float LevelMultiplier
    //{
    //    get { return 1 + (currentLevel - 1) * levelBuff; }
    //}

    //public void UpdateEXP(int point)
    //{
    //    currentEXP += point;
    //    if (currentEXP >= baseEXP)
    //    {
    //        levelUp();
    //    }
    //}

    ////=============================================================�������============================================================
    //private void levelUp()
    //{
    //    currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
    //    baseEXP = (int)(baseEXP + (currentLevel - 1) * baseEXPBuff);
    //    maxHealth = maxHealth + 10;
    //    //���꼶Ҫ��Ҫ��Ѫ
    //    currentHealth = maxHealth;
    //    currentEXP = 0;
    //}

}
