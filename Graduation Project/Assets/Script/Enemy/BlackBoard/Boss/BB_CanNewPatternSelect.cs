using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/CanNewPatternSelect")]

public class BB_CanNewPatternSelect : GOCondition
{
    [InParam("Is Pattern End")]
    public bool isPatternEnd;

    public override bool Check()
    {
        return isPatternEnd;
    }
}