using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/WhatPattern")]

public class BB_WhatPattern : GOCondition
{
    [InParam("Cur Pattern")]
    public BossAttackPattern curPattern;

    [InParam("Goal Pattern")]
    public BossAttackPattern goalPattern;

    public override bool Check()
    {
        return curPattern == goalPattern;
    }
}