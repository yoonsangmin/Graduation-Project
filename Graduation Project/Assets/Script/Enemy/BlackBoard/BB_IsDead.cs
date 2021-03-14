using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/IsDead")]

public class BB_IsDead : GOCondition
{
    [InParam("EnemyMain")]
    public Enemy enemyMain;

    [InParam("IsTrue")]
    public bool isTrue;

    public override bool Check()
    {
        return enemyMain._isDead == false || isTrue;
    }
}