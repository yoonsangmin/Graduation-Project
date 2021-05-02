using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;

[Action("BossActions / JumpAttack")]

public class BB_JumpAttack : GOAction
{
    [InParam("target")]
    public GameObject target;

    [InParam("Boss Enemy")]
    public BossEnemy boss;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        boss.JumpAttackStart();
        //navAgent.velocity = boss.aa(navAgent.transform.position, target.transform.position, 45.0f);
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {

        return TaskStatus.RUNNING;
        //공격 끝
        return TaskStatus.COMPLETED;
    }
}