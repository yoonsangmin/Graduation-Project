using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("MyActions / Boom")]

public class BB_Boom : BasePrimitiveAction
{
    [InParam("Boomer Enemy")]
    public BoomerEnemy boomerEnemy;

    public override TaskStatus OnUpdate()
    {
        boomerEnemy.BoomAction();
        return TaskStatus.COMPLETED;
    }
}