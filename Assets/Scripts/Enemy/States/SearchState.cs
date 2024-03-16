using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState :EnemyBaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.Lastpos);
    }
    public override void Perform()
    {
       if(enemy.CanSeePlayer())
       stateMachine.ChangeState(new AttackState());
       if(enemy.Agent.remainingDistance<enemy.Agent.stoppingDistance)
       {
            searchTimer+=Time.deltaTime;
            moveTimer+=Time.deltaTime;
             if (moveTimer > Random.Range(4, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + Random.insideUnitSphere * 10);
                moveTimer = 0;
            }
            if(searchTimer>Random.Range(4,10))
            {
                stateMachine.ChangeState(new PatrolState());
            }
       }
    }
    public override void Exit()
    {
        
    }
}
