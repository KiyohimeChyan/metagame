using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStats : MonoBehaviour
{
    [Header("½ÇÉ«¹¥»÷Êý¾Ý")]
    public AttackData_SO templateData;
    [HideInInspector]
    public AttackData_SO attackData;

    [HideInInspector]
    public bool isCritical;
    private void Awake()
    {
        if (templateData != null)
        {
            attackData = Instantiate(templateData);
        }
    }
    public int MinDamage
    {
        get
        {
            if (attackData != null)
                return attackData.minDamage;
            else return 0;
        }
        set
        {
            attackData.minDamage = value;
        }
    }

    public int MaxDamage
    {
        get
        {
            if (attackData != null)
                return attackData.maxDamage;
            else return 0;
        }
        set
        {
            attackData.maxDamage = value;
        }
    }

    public float AttackRange
    {
        get
        {
            if (attackData != null)
                return attackData.attackRange;
            else return 0;
        }
        set
        {
            attackData.attackRange = value;
        }
    }

    public float SkillRange
    {
        get
        {
            if (attackData != null)
                return attackData.skillRange;
            else return 0;
        }
        set
        {
            attackData.skillRange = value;
        }
    }

    public float CoolDown
    {
        get
        {
            if (attackData != null)
                return attackData.coolDown;
            else return 0;
        }
        set
        {
            attackData.coolDown = value;
        }
    }

    public float CriticalDamage
    {
        get
        {
            if (attackData != null)
                return attackData.criticalDamage;
            else return 0;
        }
        set
        {
            attackData.criticalDamage = value;
        }
    }

    public float CriticalChance
    {
        get
        {
            if (attackData != null)
                return attackData.criticalChance;
            else return 0;
        }
        set
        {
            attackData.criticalChance = value;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon"))
        {
            //Debug.Log(this.name+"¹¥»÷ÁË"+collision.name);
            collision.GetComponent<CharacterStats>().TakeDamage(this);
        }
    }

    public int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);

        if (isCritical)
        {
            coreDamage *= attackData.criticalDamage;
            //Debug.Log("±©»÷£º" + coreDamage);
        }

        return (int)coreDamage;
    }

}
