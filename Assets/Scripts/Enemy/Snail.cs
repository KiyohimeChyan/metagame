using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : EnemyController
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new SnailPatrolState();
        skillState = new SnailSkillState();
    }
}
