using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;

[Action("BossActions / JumpAttack")]

public class BB_JumpAttack : GOAction
{
    //[InParam("Boss Enemy")]
    //public BossE weapon;

    [InParam("target")]
    public GameObject target;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        //공격 중
        return TaskStatus.RUNNING;
        //공격 끝
        return TaskStatus.COMPLETED;
    }
}