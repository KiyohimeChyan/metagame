using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : EnemyController
{
    public float patrolRadius;
    Vector3 spwanPoint;

    protected override void Awake()
    {
        base.Awake();
        patrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
        spwanPoint = transform.position;
    }

    public override void EnemyMove()
    {

    }

    public override bool FoundPlayer()
    {
        var obj = Physics2D.OverlapCircle(transform.position, checkDistance, attackLayer);
        if (obj != null)
        {
            attacker = obj.transform;
        }
        return obj;
    }

    public override Vector3 GetNewPoint()
    {
        return spwanPoint + new Vector3(Random.Range(-patrolRadius, patrolRadius), Random.Range(-patrolRadius, patrolRadius));


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

}
