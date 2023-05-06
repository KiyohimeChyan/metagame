using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BaseState
{
    private AttackStats attack;
    float attackRateCounter;
    Vector3 target;
    Vector3 moveDir;
    bool isAttack;
    public override void OnEnter(EnemyController enemy)
    {
        attack = enemy.GetComponent<AttackStats>();
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.lostTime = currentEnemy.lostTimer;
        currentEnemy.anim.SetBool("isChase", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.lostTime <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        attackRateCounter -= Time.deltaTime;

        target = new Vector3(currentEnemy.attacker.position.x, currentEnemy.attacker.position.y + 1.5f, 0);

        //ÅÐ¶Ï¹¥»÷¾àÀë
        if (Mathf.Abs(target.x - currentEnemy.transform.position.x) <= attack.AttackRange && Mathf.Abs(target.y - currentEnemy.transform.position.y) <= attack.AttackRange)
        {
            //¹¥»÷
            isAttack = true;
            if (!currentEnemy.isHurt)
                currentEnemy.rb.velocity = Vector2.zero;

            if (attackRateCounter <= 0)
            {
                currentEnemy.anim.SetTrigger("attack");
                attackRateCounter = attack.CoolDown;
            }
        }
        else    //³¬³ö¹¥»÷·¶Î§
        {
            isAttack = false;
        }

        moveDir = (target - currentEnemy.transform.position).normalized;

        if (moveDir.x > 0)
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        if (moveDir.x < 0)
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);
    }

    public override void PhysicsUpdate()
    {
        if (!currentEnemy.isHurt && !currentEnemy.isDead&&!isAttack)
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.fixedDeltaTime;
        }
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("isChase", false);

    }
}
