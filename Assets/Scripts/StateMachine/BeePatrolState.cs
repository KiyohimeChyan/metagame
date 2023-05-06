using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BaseState
{
    Vector3 target;
    Vector3 faceDir;

    public override void OnEnter(EnemyController enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        target = currentEnemy.GetNewPoint();

    }
    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
        if(Mathf.Abs(target.x-currentEnemy.transform.position.x)<0.1f&& Mathf.Abs(target.y - currentEnemy.transform.position.y) < 0.1f)
        {
            currentEnemy.isWait = true;
            target = currentEnemy.GetNewPoint();
        }
        faceDir = (target - currentEnemy.transform.position).normalized;
        if (faceDir.x > 0)
        {
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (faceDir.x < 0)
        {
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);
        }

    }



    public override void PhysicsUpdate()
    {
        if (!currentEnemy.isHurt && !currentEnemy.isDead && !currentEnemy.isWait)
        {
            currentEnemy.rb.velocity = faceDir * currentEnemy.currentSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentEnemy.rb.velocity = Vector3.zero;
        }


    }
    public override void OnExit()
    {
    }
}
