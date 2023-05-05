using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : EnemyController
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    }
}
