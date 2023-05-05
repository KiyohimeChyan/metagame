using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSkillState : BaseState
{
    public override void OnEnter(EnemyController enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("isWalk", false);
        currentEnemy.anim.SetBool("isHide", true);
        currentEnemy.anim.SetTrigger("skill");
        currentEnemy.lostTime = currentEnemy.lostTimer;
        currentEnemy.GetComponent<CharacterStats>().isInvincible = true;
        currentEnemy.GetComponent<CharacterStats>().invincibleTimer = currentEnemy.lostTime;
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.lostTime <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        currentEnemy.GetComponent<CharacterStats>().invincibleTimer = currentEnemy.lostTime;

    }
    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("isHide", false);
        currentEnemy.GetComponent<CharacterStats>().isInvincible = false;

    }
}
