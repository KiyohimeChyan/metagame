using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    [Header("��ɫ��������")]
    public CharaterData_SO templateData;
    //��ȡso����Ľ�ɫ����
    [HideInInspector]
    public CharaterData_SO characterData;

    [HideInInspector]
    public bool isCritical;
    float invincibleTimer;
    bool isInvincible;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;

    public int MaxHealth
    {
        get
        {
            if (characterData != null)
                return characterData.maxHealth;
            else return 0;
        }
        set
        {
            characterData.maxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (characterData != null)
                return characterData.currentHealth;
            else return 0;
        }
        set
        {
            characterData.currentHealth = value;
        }
    }

    public int BaseDeffence
    {
        get
        {
            if (characterData != null)
                return characterData.baseDeffence;
            else return 0;
        }
        set
        {
            characterData.baseDeffence = value;
        }
    }

    public int CurrentDeffence
    {
        get
        {
            if (characterData != null)
                return characterData.currentDeffence;
            else return 0;
        }
        set
        {
            characterData.currentDeffence = value;
        }
    }

    public float InvincibleTime
    {
        get
        {
            if (characterData != null)
                return characterData.invincibleTime;
            else return 0;
        }
        set
        {
            characterData.invincibleTime = value;
        }
    }

    private void Awake()
    {
        if (templateData != null)
        {
            characterData = Instantiate(templateData);
        }
    }
    private void Update()
    {
        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer <= 0)
        {
            isInvincible = false;
        }
    }

    public void TakeDamage(AttackStats attacker)
    {
        if (isInvincible)
            return;
        int damage = Mathf.Max(attacker.CurrentDamage() - CurrentDeffence, 0);
        if (CurrentHealth > damage)
        {
            CurrentHealth -= damage;
            TriggerInvincible();
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            CurrentHealth = 0;
            OnDie?.Invoke();
        }
        Debug.Log(CurrentHealth);
    }

    private void TriggerInvincible()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            invincibleTimer = InvincibleTime;
        }
    }

    //public void TakeDamageA(CharacterStats attacker, CharacterStats defender)
    //{
    //    int damage = Mathf.Max(attacker.CurrentDamage() - defender.CurrentDeffence,0);
    //    CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
    //    defender.GetComponent<Animator>().SetBool          //�ܻ���������
    //Debug.Log(CurrentHealth);
    //    if (CurrentHealth <= 0)
    //    {
    //        attacker.characterData.UpdateEXP(characterData.point);
    //    }
    //}

    ////=============================================�������========================================================
    //public void TakeDamageSkill01(CharacterStats attacker, CharacterStats defender)
    //{
    //    int damage = (int)(Mathf.Max(attacker.CurrentDamage() - defender.CurrentDeffence, 0)*(0.8 + attacker.characterData.currentLevel * 0.1));
    //    CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
    //    //defender.GetComponent<Animator>().SetBool          //�ܻ���������
    //    if (CurrentHealth <= 0)
    //    {
    //        attacker.characterData.UpdateEXP(characterData.point);
    //    }
    //}

    //public void TakeDamageSkill02(CharacterStats attacker, CharacterStats defender)
    //{
    //    int damage = (int)(Mathf.Max(attacker.CurrentDamage() - defender.CurrentDeffence, 0) * (0.6 + attacker.characterData.currentLevel * 0.05));
    //    CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
    //    //defender.GetComponent<Animator>().SetBool          //�ܻ���������
    //    if (CurrentHealth <= 0)
    //    {
    //        attacker.characterData.UpdateEXP(characterData.point);
    //    }
    //}


    //private int CurrentDamage()
    //{
    //    float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);

    //    if (isCritical)
    //    {
    //        coreDamage *= attackData.criticalDamage;
    //        Debug.Log("������" + coreDamage);
    //    }

    //    return (int)coreDamage;
    //}


    //========================================����ǿ����ѡһ======================================
    //public void UpgradeAttackDamage()
    //{
    //    MinDamage += 10;
    //    MaxDamage += 10;
    //    Debug.Log("�ӹ�����");
    //}

    //public void UpgradeAttackRange()
    //{
    //    AttackRange = AttackRange + 0.2f;
    //    Debug.Log("�ӹ�������");
    //}

    //public void UpgradeAttackCoolDown()
    //{
    //    CoolDown *= 0.95f;
    //    Debug.Log("���̹������");
    //}

    //public void UpgradeCriticalChance()
    //{
    //    CriticalChance += 0.08f;
    //    Debug.Log("���ӱ�����");
    //}
}
